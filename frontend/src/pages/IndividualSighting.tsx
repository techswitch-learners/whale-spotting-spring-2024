import { useContext, useState, useEffect } from "react"
import { useParams } from "react-router-dom"
import { Row, Col, Container } from "react-bootstrap"
import { BackgroundContext } from "../App"
import { getSightingById } from "../api/backendClient"
//import Sighting from "../models/request/AddSightingRequest"

interface Species {
  id: number | null
  name: string | null
  exampleImageUrl: string | null
  wikiLink: string | null
}
interface VerificationEvent {
  id: number | null
  sightingId: number | null
  adminId: number | null
  comment: string | null
  timeStamp: Date | null
}
interface Reaction {
  id: number | null
  reactionType: string | null
  userId: number | null
  sightingId: number | null
  timeStamp: Date | null
}
interface GetSighting {
  id: number | null
  latitude: number | null
  longitude: number | null
  userName: string | null
  species: Species | null
  description: string | null
  imageUrl: string | null
  bodyOfWaterName: string | null
  verificationEvent: VerificationEvent | null
  sightingTimestamp: Date | null
  creationTimestamp: Date | null
  reactions: Reaction[] | null
}

const IndividualSighting = () => {
  //const response= await fetch(`http://localhost:3001/users/${id}`);
  const { id } = useParams()
  const backgroundContext = useContext(BackgroundContext)
  const [sighting, setSighting] = useState<GetSighting>()
  //const [loading, setLoading] = useState<boolean>(false)
  //const [success, setSuccess] = useState<boolean>(false)
  const [error, setError] = useState<boolean>(false)

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  useEffect(() => {
    getSightingById(id)
      .then((response) => response.json())
      .then((content) => {
        setSighting(content)
        console.log(content)
      })
      .catch(() => setError(true))
  }, [id])

  return (
    <>
      <Container>
        <Row>
          <Col>{sighting?.latitude}</Col>
          <Col>2 of 2</Col>
        </Row>
        <Row>
          <Col>1 of 3</Col>
          <Col>2 of 3</Col>
          <Col>3 of 3</Col>
        </Row>
      </Container>
      {error && <p>Error fetching sighting from the backend</p>}
    </>
  )
}

export default IndividualSighting
