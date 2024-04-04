import { useContext, useEffect, useState } from "react"
import Sighting from "../models/view/Sighting"
import { getPendingSightings, verifySighting } from "../api/backendClient"
import { AuthContext, BackgroundContext } from "../App"
import { Button, Card, Form, FormGroup } from "react-bootstrap"
import Error403 from "./Error403"

interface SightingCardProps {
  sighting: Sighting
  handleSubmit: (id: number, status: string, setComment: (comment?: string) => void, comment?: string) => void
}
function PendingSightingCard({ sighting, handleSubmit }: SightingCardProps) {
  const [comment, setComment] = useState<string>()
  const handleCommentChange = (value: string) => {
    setComment(value)
  }

  return (
    <Card className="text-start" style={{ width: "15rem" }}>
      <Card.Img variant="top" src={sighting.imageUrl} style={{ height: "15rem", objectFit: "cover" }} />
      <Card.Body>
        <Card.Text>Posted by: {sighting.userName}</Card.Text>
        <Card.Text>Species: {sighting.species.name}</Card.Text>
        <Card.Text>Body of Water: {sighting.bodyOfWater}</Card.Text>
        <Card.Text>Description: {sighting.description}</Card.Text>
        <Card.Text>Posted on: {sighting.sightingTimestamp.split("T")[0]}</Card.Text>
      </Card.Body>
      <Card.Footer className="d-flex justify-content-center align-items-center">
        <Form>
          <FormGroup controlId={`commentTextArea${sighting.id}`} className="mb-2 text-start">
            <Form.Label>Comment(optional):</Form.Label>
            <Form.Control
              as="textarea"
              placeholder="Leave your comments"
              value={comment}
              onChange={(e) => handleCommentChange(e.target.value)}
            />
          </FormGroup>
          <Button onClick={() => handleSubmit(sighting.id, "approve", setComment, comment)}>Approve</Button>
          <Button
            className="mx-3"
            variant="danger"
            onClick={() => handleSubmit(sighting.id, "approve", setComment, comment)}
          >
            Reject
          </Button>
        </Form>
      </Card.Footer>
    </Card>
  )
}

const PendingSightings = () => {
  const backgroundContext = useContext(BackgroundContext)
  const [pendingSightings, setPendingSightings] = useState<Sighting[]>()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)
  const [unauthorisedAccess, setUnauthorisedAccess] = useState(false)
  const authContext = useContext(AuthContext)

  const handleSubmit = (id: number, status: string, setComment: (comment?: string) => void, comment?: string) => {
    setError(false)
    const approvalStatus = status === "approve" ? 1 : 0

    verifySighting(
      {
        comment,
        approvalStatus,
      },
      id,
      authContext.cookie.token,
    )
      .then((response) => {
        if (response.ok) {
          setComment(undefined)
          setPendingSightings(pendingSightings?.filter((sighting) => sighting.id != id))
        } else if (response.status === 403 || response.status === 401) {
          authContext.removeCookie("token")
          setUnauthorisedAccess(true)
        }
      })
      .catch(() => setError(true))
  }

  function getData() {
    setLoading(true)
    setError(false)
    getPendingSightings(authContext.cookie.token)
      .then((response) => {
        console.log(response.status)
        if (response.ok) {
          response.json().then((data) => setPendingSightings(data.sightings))
        } else if (response.status === 403 || response.status === 401) {
          authContext.removeCookie("token")
          setUnauthorisedAccess(true)
        }
      })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  useEffect(getData, [authContext])

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  return (
    <div className="d-flex flex-column text-center">
      {pendingSightings && (
        <>
          <h1>Pending Sightings</h1>
          <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
            {pendingSightings.map((sighting) => (
              <PendingSightingCard key={sighting.id} sighting={sighting} handleSubmit={handleSubmit} />
            ))}
          </div>
        </>
      )}

      {unauthorisedAccess && <Error403 />}
      {loading && <p>Loading...</p>}
      {error && <p>There was an error</p>}
    </div>
  )
}

export default PendingSightings
