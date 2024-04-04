import { useContext, useEffect, useState } from "react"
import Sighting from "../models/view/Sighting"
import { deleteSighting, editApprovalStatus, getRejectedSightings } from "../api/backendClient"
import { AuthContext, BackgroundContext } from "../App"
import { Button, Card } from "react-bootstrap"
import Error403 from "./Error403"

interface RejectedSightingCardProps {
  sighting: Sighting
  handleRestore: (id: number) => void
  handleDeleteSighting: (id: number) => void
}
function RejectedSightingCard({ sighting, handleRestore, handleDeleteSighting }: RejectedSightingCardProps) {
  return (
    <Card className="text-start" style={{ width: "15rem" }}>
      <Card.Img variant="top" src={sighting.imageUrl} style={{ height: "15rem", objectFit: "cover" }} />
      <Card.Body>
        <Card.Text>Posted by: {sighting.userName}</Card.Text>
        <Card.Text>Species: {sighting.species.name}</Card.Text>
        <Card.Text>Body of Water: {sighting.bodyOfWater}</Card.Text>
        <Card.Text>Description: {sighting.description}</Card.Text>
        <Card.Text>Posted on: {sighting.sightingTimestamp.split("T")[0]}</Card.Text>
        <Card.Text>Admin: {sighting.verificationEvent?.admin.userName}</Card.Text>
        <Card.Text>
          Comment: {sighting.verificationEvent ? sighting.verificationEvent.comment : "The admin left no comment"}
        </Card.Text>
        <Card.Text>Rejected on: {sighting.verificationEvent?.timestamp.split("T")[0]}</Card.Text>
      </Card.Body>
      <Card.Footer className="d-flex justify-content-center align-items-center">
        <Button className="mx-2" onClick={() => handleRestore(sighting.id)}>
          Restore
        </Button>
        <Button className="mx-2" variant="danger" onClick={() => handleDeleteSighting(sighting.id)}>
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
