import { useContext, useEffect, useState } from "react"
import { deleteSighting, editApprovalStatus, getRejectedSightings } from "../api/backendClient"
import { AuthContext, BackgroundContext } from "../App"
import { Button, Card } from "react-bootstrap"
import Error403 from "./Error403"
import Sighting from "../models/view/Sighting"
import { Link } from "react-router-dom"

interface RejectedSightingCardProps {
  sighting: Sighting
  handleRestore: (id: number) => void
  handleDeleteSighting: (id: number) => void
}
function RejectedSightingCard({ sighting, handleRestore, handleDeleteSighting }: RejectedSightingCardProps) {
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
        <Card.Text className="mb-0">Posted on {sighting.creationTimestamp.split("T")[0]}</Card.Text>
        <Card.Text>Rejected on {sighting.verificationEvent.timestamp.split("T")[0]}</Card.Text>
        <hr />
        <Card.Text className="mb-0">Rejected by {sighting.verificationEvent.admin.userName}</Card.Text>
        <Card.Text>Comment: {sighting.verificationEvent.comment ?? "No comment provided"}</Card.Text>
      </Card.Body>
      <Card.Footer className="p-3">
        <Button className="me-2" onClick={() => handleRestore(sighting.id)}>
          Restore
        </Button>
        <Button variant="danger" onClick={() => handleDeleteSighting(sighting.id)}>
          Delete
        </Button>
      </Card.Footer>
    </Card>
  )
}

const RejectedSightings = () => {
  const backgroundContext = useContext(BackgroundContext)
  const [rejectedSightings, setRejectedSightings] = useState<Sighting[]>()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)
  const [unauthorisedAccess, setUnauthorisedAccess] = useState(false)

  const authContext = useContext(AuthContext)

  function handleDeleteSighting(id: number) {
    deleteSighting(id, authContext.cookie.token).then((response) => {
      if (response.ok) {
        setRejectedSightings(rejectedSightings?.filter((sighting) => sighting.id != id))
      }
    })
  }

  function handleRestore(id: number) {
    editApprovalStatus(id, authContext.cookie.token).then((response) => {
      if (response.ok) {
        setRejectedSightings(rejectedSightings?.filter((sighting) => sighting.id != id))
      }
    })
  }

  function getData() {
    setLoading(true)
    setError(false)
    getRejectedSightings(authContext.cookie.token)
      .then((response) => {
        console.log(response.status)
        if (response.ok) {
          response.json().then((data) => setRejectedSightings(data.sightings))
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
      {rejectedSightings && (
        <>
          <h1>Rejected Sightings</h1>
          <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
            {rejectedSightings.map((sighting) => (
              <RejectedSightingCard
                key={sighting.id}
                sighting={sighting}
                handleDeleteSighting={handleDeleteSighting}
                handleRestore={handleRestore}
              />
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

export default RejectedSightings
