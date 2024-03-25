import { useContext, useState, useEffect } from "react"
import { useParams } from "react-router-dom"
import { Row, Col, Container, Image } from "react-bootstrap"
import { BackgroundContext } from "../App"
import { getSightingById } from "../api/backendClient"
import Sighting from "../models/view/Sighting"

const IndividualSighting = () => {
  //const response= await fetch(`http://localhost:3001/users/${id}`);
  const { id } = useParams()
  const backgroundContext = useContext(BackgroundContext)
  const [sighting, setSighting] = useState<Sighting>()
  const [loading, setLoading] = useState<boolean>(false)
  // const [success, setSuccess] = useState<boolean>(false)
  const [error, setError] = useState<boolean>(false)

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  useEffect(() => {
    setLoading(true)
    setError(false)
    getSightingById(id)
      .then((response) => response.json())
      .then((data) => {
        setSighting(data)
        console.log(data)
      })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }, [id])

  return (
    <>
      <Container>
        <Row>
          <Col xs={8} md={6}>
            <Image src={sighting?.imageUrl} fluid rounded />
          </Col>
        </Row>
        <Row>
          <Col>
            <p>Spotted By:</p>
          </Col>
          <Col>{sighting?.userName}</Col>
        </Row>
        <Row>
          <Col>
            <p>
              Spotted At: {sighting?.latitude} (Latitude) {sighting?.longitude} (Longitude) {sighting?.bodyOfWater.name}
            </p>
          </Col>
        </Row>
        <Row>
          <Col>
            <p>
              Species:
              <a href={sighting?.species.wikiLink} target="_blank">
                {sighting?.species.name}
              </a>
            </p>
          </Col>
        </Row>
      </Container>
      {loading && <p>Loading...</p>}
      {error && <p>Error fetching sighting from the backend</p>}
    </>
  )
}

export default IndividualSighting
