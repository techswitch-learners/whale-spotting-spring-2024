import { useContext, useState, useEffect } from "react"
import { useParams } from "react-router-dom"
import { Container, Card, Row, Col, Stack, Button } from "react-bootstrap"
import { BackgroundContext } from "../App"
import { getSightingById } from "../api/backendClient"
import Sighting from "../models/view/Sighting"
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet"
import { Icon } from "leaflet"
import icon from "/favicon.ico"

const customIcon = new Icon({
  iconUrl: icon,
  iconSize: [24, 24],
})

const SightingView = () => {
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
      {sighting && (
        <Container className="pb-4" fluid>
          <Stack gap={5}>
            <Stack direction="horizontal">
              <Card.Title>{sighting?.userName}</Card.Title>
              <Button variant="primary" className="share-button ms-auto">
                Share
              </Button>{" "}
            </Stack>

            <Stack direction="horizontal" gap={3}>
              <div className="species-card" style={{ width: "30rem" }}>
                <Stack direction="horizontal" gap={1}>
                  <div style={{ backgroundColor: "green", height: "10rem", width: "2rem" }} />
                  <div>
                    <Row>
                      <Col>
                        <Card.Img src={sighting?.species.exampleImageUrl} />
                      </Col>
                      <Col>
                        <Card.Title>Species</Card.Title>
                        <Card.Link href={sighting?.species.wikiLink} target="_blank">
                          {sighting?.species.name}
                        </Card.Link>
                      </Col>
                    </Row>
                  </div>
                </Stack>
              </div>
              <div className="sighting-card" style={{ width: "60rem" }}>
                <Card>
                  <Card.Img variant="top" src={sighting?.imageUrl} />
                  <Card.Body>
                    <Card.Text>{sighting?.description}</Card.Text>
                  </Card.Body>
                </Card>
              </div>
            </Stack>
            <Stack direction="horizontal" gap={3}>
              <div className="location-card" style={{ width: "30rem" }}>
                <Stack direction="horizontal" gap={1}>
                  <div style={{ backgroundColor: "green", height: "10rem", width: "0.5rem" }} />
                  <div>
                    <Card.Title>Sight Location</Card.Title>
                    <Card.Subtitle>{sighting?.bodyOfWater.name}</Card.Subtitle>
                  </div>
                </Stack>
              </div>
              <div className="map-card" style={{ width: "60rem" }}>
                <div style={{ height: "30rem", width: "100%" }}>
                  <MapContainer
                    style={{ height: "100%" }}
                    center={[sighting?.latitude, sighting?.longitude]}
                    zoom={3}
                    scrollWheelZoom={true}
                  >
                    <TileLayer
                      attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                      url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                    />

                    <Marker
                      key={sighting?.id}
                      position={[Number(sighting?.latitude), Number(sighting?.longitude)]}
                      icon={customIcon}
                    >
                      <Popup>{sighting?.description}</Popup>
                    </Marker>
                  </MapContainer>
                </div>
              </div>
            </Stack>
          </Stack>
        </Container>
      )}

      {/* {sighting && (
        <Container className="pb-4">
          <Card className="text-start">
            <Card.Img variant="top" src={sighting?.imageUrl} />
            <Card.Body>
              <Card.Title>{sighting?.species.name}</Card.Title>
              <Card.Subtitle>{sighting?.bodyOfWater.name}</Card.Subtitle>
              <Card.Text>{sighting?.description}</Card.Text>
              <div style={{ height: "20rem", width: "50%" }}>
                <MapContainer
                  style={{ height: "100%" }}
                  center={[sighting?.latitude, sighting?.longitude]}
                  zoom={3}
                  scrollWheelZoom={true}
                >
                  <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                  />

                  <Marker
                    key={sighting?.id}
                    position={[Number(sighting?.latitude), Number(sighting?.longitude)]}
                    icon={customIcon}
                  >
                    <Popup>{sighting?.description}</Popup>
                  </Marker>
                </MapContainer>
              </div>
            </Card.Body>
            <Card.Footer>
              <small>{sighting?.sightingTimestamp}</small>
            </Card.Footer>
          </Card>
        </Container>
      )} */}
      {loading && <p>Loading...</p>}
      {error && <p>Error fetching sighting from the backend</p>}
    </>
  )
}

export default SightingView
