import { useContext, useEffect, useState } from "react"
import Sighting from "../models/view/Sighting"
import { getPendingSightings } from "../api/backendClient"
import { AuthContext } from "../App"
import { Button, Card } from "react-bootstrap"

const PendingSightings = () => {
  const [pendingSightings, setPendingSightings] = useState<Sighting[]>()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)
  const [unauthorisedAccess, setUnauthorisedAccess] = useState(false)
  const authContext = useContext(AuthContext)

  interface SightingCardProps {
    imageUrl: string
    species: string
    bodyOfWater: string
    description: string
    sightingTimestamp: string
  }
  function PendingSightingCard({ imageUrl, species, bodyOfWater, description, sightingTimestamp }: SightingCardProps) {
    return (
      <Card className="text-start">
        <Card.Img variant="top" src={imageUrl} />
        <Card.Body>
          <Card.Text>Species: {species}</Card.Text>
          <Card.Text>Body of Water: {bodyOfWater}</Card.Text>
          <Card.Text>Description: {description}</Card.Text>
          <Card.Text>Timestamp: {sightingTimestamp}</Card.Text>
        </Card.Body>
        <Card.Footer className="d-flex justify-content-center">
          <Button className="mx-4">Approve</Button>
          <Button className="mx-4">Reject</Button>
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
          response
            .json()
            .then((data) => setPendingSightings(data.sightings))
            .then((data) => console.log(data))
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
    <div className="SightingsSearch d-flex flex-column text-center">
      <h1>Pending Sightings</h1>
      {pendingSightings && (
        <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
          {pendingSightings.map((sighting) => (
            <PendingSightingCard
              imageUrl={sighting.imageUrl}
              species={sighting.species.name}
              bodyOfWater={sighting.bodyOfWater}
              description={sighting.description}
              sightingTimestamp={"Observed on " + sighting.sightingTimestamp.split("T")[0]}
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
