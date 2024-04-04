import { useContext, useEffect, useState } from "react"
import Sighting from "../models/view/Sighting"
import { getPendingSightings, verifySighting } from "../api/backendClient"
import { AuthContext, BackgroundContext } from "../App"
import { Button, Card, Form, FormGroup } from "react-bootstrap"

const PendingSightings = () => {
  const backgroundContext = useContext(BackgroundContext)
  const [pendingSightings, setPendingSightings] = useState<Sighting[]>()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)
  const [unauthorisedAccess, setUnauthorisedAccess] = useState(false)
  const [comments, setComments] = useState<string[]>([])
  const authContext = useContext(AuthContext)

  interface SightingCardProps {
    id: number
    index: number
    imageUrl: string
    userName: string
    species: string
    bodyOfWater: string
    description: string
    sightingTimestamp: string
  }
  function PendingSightingCard({
    id,
    index,
    imageUrl,
    species,
    bodyOfWater,
    description,
    sightingTimestamp,
    userName,
  }: SightingCardProps) {
    const handleCommentChange = (value: string, index: number) => {
      const newComments = [...comments]
      newComments[index] = value
      setComments(newComments)
    }

    const handleSubmit = (status: string, id: number, index: number) => {
      setError(false)
      const approvalStatus = status === "approve" ? 1 : 0
      const comment = comments[index]

      verifySighting(
        {
          comment: comment ? comment : undefined,
          approvalStatus: approvalStatus,
        },
        id,
        authContext.cookie.token,
      )
        .then((response) => {
          if (response.ok) {
            setComments([])
            setPendingSightings(pendingSightings?.filter((sighting) => sighting.id != id))
          } else if (response.status === 403 || response.status === 401) {
            authContext.removeCookie("token")
            setUnauthorisedAccess(true)
          }
        })
        .catch(() => setError(true))
    }

    return (
      <Card className="text-start" style={{ width: "15rem" }}>
        <Card.Img variant="top" src={imageUrl} style={{ height: "15rem", objectFit: "cover" }} />
        <Card.Body>
          <Card.Text>Posted by: {userName}</Card.Text>
          <Card.Text>Species: {species}</Card.Text>
          <Card.Text>Body of Water: {bodyOfWater}</Card.Text>
          <Card.Text>Description: {description}</Card.Text>
          <Card.Text>Posted on: {sightingTimestamp}</Card.Text>
        </Card.Body>
        <Card.Footer className="d-flex justify-content-center align-items-center">
          <Form>
            <FormGroup controlId={`commentTextArea${id}`} className="mb-2 text-start">
              <Form.Label>Comment(optional):</Form.Label>
              <Form.Control
                as="textarea"
                placeholder="Leave your comments"
                value={comments[index]}
                onChange={(e) => handleCommentChange(e.target.value, index)}
              />
            </FormGroup>
            <Button onClick={() => handleSubmit("approve", id, index)}>Approve</Button>
            <Button className="mx-3" variant="danger" onClick={() => handleSubmit("reject", id, index)}>
              Reject
            </Button>
          </Form>
        </Card.Footer>
      </Card>
    )
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
      <h1>Pending Sightings</h1>
      {pendingSightings && (
        <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
          {pendingSightings.map((sighting, index) => (
            <PendingSightingCard
              key={sighting.id}
              id={sighting.id}
              index={index}
              userName={sighting.userName}
              imageUrl={sighting.imageUrl}
              species={sighting.species.name}
              bodyOfWater={sighting.bodyOfWater}
              description={sighting.description}
              sightingTimestamp={sighting.sightingTimestamp.split("T")[0]}
            />
          ))}
        </div>
      )}

      {unauthorisedAccess && <p>You shouldn't be here</p>}
      {loading && <p>Loading...</p>}
      {error && <p>There was an error</p>}
    </div>
  )
}

export default PendingSightings
