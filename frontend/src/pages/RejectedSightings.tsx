import { useContext, useEffect, useState } from "react"
import Sighting from "../models/view/Sighting"
import { deleteSighting, editApprovalStatus, getRejectedSightings } from "../api/backendClient"
import { AuthContext } from "../App"
import { Button, Card } from "react-bootstrap"

const RejectedSightings = () => {
  const [rejectedSightings, setRejectedSightings] = useState<Sighting[]>()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)
  const [unauthorisedAccess, setUnauthorisedAccess] = useState(false)

  const authContext = useContext(AuthContext)

  interface RejectedSightingCardProps {
    id: number
    imageUrl: string
    userName: string
    species: string
    bodyOfWater: string
    description: string
    sightingTimestamp: string
    adminUserName: string
    comment: string | null
  }
  function RejectedSightingCard({
    id,
    imageUrl,
    species,
    bodyOfWater,
    description,
    sightingTimestamp,
    userName,
    adminUserName,
    comment,
  }: RejectedSightingCardProps) {
    function handleDeleteSighting(id: number, authContext: string | undefined) {
      deleteSighting(id, authContext).then((response) => {
        if (response.ok) {
          setRejectedSightings(rejectedSightings?.filter((sighting) => sighting.id != id))
        }
      })
    }

    function handleRestore(id: number, authContext: string | undefined) {
      editApprovalStatus(id, authContext).then((response) => {
        if (response.ok) {
          setRejectedSightings(rejectedSightings?.filter((sighting) => sighting.id != id))
        }
      })
    }

    return (
      <Card className="text-start" style={{ width: "15rem" }}>
        <Card.Img variant="top" src={imageUrl} style={{ height: "15rem", objectFit: "cover" }} />
        <Card.Body>
          <Card.Text>Posted by: {userName}</Card.Text>
          <Card.Text>Species: {species}</Card.Text>
          <Card.Text>Body of Water: {bodyOfWater}</Card.Text>
          <Card.Text>Description: {description}</Card.Text>
          <Card.Text>Timestamp: {sightingTimestamp}</Card.Text>
          <Card.Text>Responsible admin: {adminUserName}</Card.Text>
          <Card.Text>Comment: {comment}</Card.Text>
        </Card.Body>
        <Card.Footer className="d-flex justify-content-center align-items-center">
          <Button className="mx-2" onClick={() => handleRestore(id, authContext.cookie.token)}>
            Restore
          </Button>
          <Button className="mx-2" variant="danger" onClick={() => handleDeleteSighting(id, authContext.cookie.token)}>
            Delete
          </Button>
        </Card.Footer>
      </Card>
    )
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

  return (
    <div className="d-flex flex-column text-center">
      <h1>Rejected Sightings</h1>
      {rejectedSightings && (
        <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
          {rejectedSightings.map((sighting) => (
            <RejectedSightingCard
              key={sighting.id}
              id={sighting.id}
              userName={sighting.userName}
              imageUrl={sighting.imageUrl}
              species={sighting.species.name}
              bodyOfWater={sighting.bodyOfWater}
              description={sighting.description}
              sightingTimestamp={sighting.sightingTimestamp.split("T")[0]}
              adminUserName={sighting.verificationEvent?.admin.userName}
              comment={sighting.verificationEvent ? sighting.verificationEvent.comment : "The admin left no comment"}
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

export default RejectedSightings
