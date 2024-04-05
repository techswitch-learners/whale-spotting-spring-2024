import { useContext, useEffect, useState } from "react"
import { Button, Card, Spinner } from "react-bootstrap"
import { Link, Navigate, useNavigate } from "react-router-dom"
import { AuthContext, BackgroundContext } from "../App"
import { deleteSighting, editApprovalStatus, getRejectedSightings } from "../api/backendClient"
import Sighting from "../models/view/Sighting"
import Error403 from "./Error403"

interface RejectedSightingCardProps {
  sighting: Sighting
  handleRestore: (id: number) => void
  handleDeleteSighting: (id: number) => void
}

const RejectedSightingCard = ({ sighting, handleRestore, handleDeleteSighting }: RejectedSightingCardProps) => {
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
        <Card.Text>Comment: {sighting.verificationEvent.comment || "No comment provided"}</Card.Text>
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
  const navigate = useNavigate()

  function handleDeleteSighting(id: number) {
    deleteSighting(id, authContext.cookie.token).then((response) => {
      if (response.ok) {
        setRejectedSightings(rejectedSightings?.filter((sighting) => sighting.id != id))
      } else if (response.status === 403) {
        setUnauthorisedAccess(true)
      } else if (response.status === 401) {
        authContext.removeCookie("token")
        navigate("/login")
      }
    })
  }

  function handleRestore(id: number) {
    editApprovalStatus(id, authContext.cookie.token).then((response) => {
      if (response.ok) {
        setRejectedSightings(rejectedSightings?.filter((sighting) => sighting.id != id))
      } else if (response.status === 403) {
        setUnauthorisedAccess(true)
      } else if (response.status === 401) {
        authContext.removeCookie("token")
        navigate("/login")
      }
    })
  }

  function getData() {
    setLoading(true)
    setError(false)
    getRejectedSightings(authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => setRejectedSightings(data.sightings))
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
      <h1>Rejected Sightings</h1>
      {rejectedSightings && (
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
      )}
      {loading && (
        <p>
          Loading...
          <Spinner />
        </p>
      )}
      {error && <p>Couldn't load data at this time</p>}
    </div>
  )
}

export default RejectedSightings
