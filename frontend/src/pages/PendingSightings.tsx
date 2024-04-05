import { useContext, useEffect, useState } from "react"
import { Button, Card, Form, FormGroup } from "react-bootstrap"
import { Link, Navigate, useNavigate } from "react-router-dom"
import { AuthContext, BackgroundContext } from "../App"
import { getPendingSightings, verifySighting } from "../api/backendClient"
import Sighting from "../models/view/Sighting"
import Error403 from "./Error403"

interface SightingCardProps {
  sighting: Sighting
  handleSubmit: (id: number, status: string, setComment: (comment: string) => void, comment: string) => void
}

const PendingSightingCard = ({ sighting, handleSubmit }: SightingCardProps) => {
  const [comment, setComment] = useState<string>("")

  return (
    <Card className="text-start" style={{ width: "15rem" }}>
      <Card.Img variant="top" src={sighting.imageUrl} style={{ height: "15rem", objectFit: "cover" }} />
      <Card.Header>
        Posted by <Link to={`/users/${sighting.userName}`}>{sighting.userName}</Link>
      </Card.Header>
      <Card.Body>
        <Card.Title>{sighting.species.name}</Card.Title>
        <Card.Subtitle>{sighting.bodyOfWater}</Card.Subtitle>
        <Card.Text>{sighting.description}</Card.Text>
        <hr />
        <Card.Text className="mb-0">Seen on {sighting.sightingTimestamp.split("T")[0]}</Card.Text>
        <Card.Text>Posted on {sighting.creationTimestamp.split("T")[0]}</Card.Text>
      </Card.Body>
      <Card.Footer className="p-3">
        <Form>
          <FormGroup controlId={`commentTextArea${sighting.id}`} className="mb-3 text-start">
            <Form.Label>Comment (optional):</Form.Label>
            <Form.Control
              as="textarea"
              placeholder="Leave your comments"
              value={comment}
              onChange={(e) => setComment(e.target.value)}
            />
          </FormGroup>
          <Button className="me-2" onClick={() => handleSubmit(sighting.id, "approve", setComment, comment)}>
            Approve
          </Button>
          <Button variant="danger" onClick={() => handleSubmit(sighting.id, "reject", setComment, comment)}>
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
  const navigate = useNavigate()

  const handleSubmit = (id: number, status: string, setComment: (comment: string) => void, comment: string) => {
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
          setComment("")
          setPendingSightings(pendingSightings?.filter((sighting) => sighting.id != id))
        } else if (response.status === 403) {
          setUnauthorisedAccess(true)
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        }
      })
      .catch(() => setError(true))
  }

  function getData() {
    setLoading(true)
    setError(false)
    getPendingSightings(authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => setPendingSightings(data.sightings))
        } else if (response.status === 403) {
          setUnauthorisedAccess(true)
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        }
      })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  useEffect(getData, [authContext, navigate])

  useEffect(() => {
    if (!unauthorisedAccess) {
      backgroundContext.setBackground("white")
    }
  }, [backgroundContext, unauthorisedAccess])

  if (!authContext.cookie.token) {
    return <Navigate to="/login" />
  }

  if (unauthorisedAccess) {
    return <Error403 />
  }

  return (
    <div className="d-flex flex-column text-center">
      <h1>Pending Sightings</h1>
      {pendingSightings && (
        <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
          {pendingSightings.map((sighting) => (
            <PendingSightingCard key={sighting.id} sighting={sighting} handleSubmit={handleSubmit} />
          ))}
        </div>
      )}

      {loading && <p>Loading...</p>}
      {error && <p>There was an error</p>}
    </div>
  )
}

export default PendingSightings
