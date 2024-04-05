import { useContext, useState, useEffect } from "react"
import { Card, Row, Col, Stack, Container, Image, Spinner } from "react-bootstrap"
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet"
import { Link, useParams } from "react-router-dom"
import { Icon } from "leaflet"
import { BackgroundContext } from "../App"
import { AuthContext } from "../App"
import { getSightingById } from "../api/backendClient"
import ReactionsCard from "../components/ReactionsCard"
import Sighting from "../models/view/Sighting"
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
  const [error, setError] = useState<boolean>(false)
  const authContext = useContext(AuthContext)

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  useEffect(() => {
    setLoading(true)
    setError(false)
    getSightingById(id, authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => setSighting(data))
        } else if (response.status === 404) {
          console.log("Sighting not found")
        } else {
          setError(true)
        }
      })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }, [id, authContext.cookie.token])

  return (
    <>
      {sighting && (
        <Container className="pb-4">
          <Stack gap={5}>
            <div className="d-flex flex-wrap justify-content-center justify-content-md-between">
              <h1 className="me-3">
                Sighting by <Link to={`/users/${sighting.userName}`}>{sighting.userName}</Link>
              </h1>
              <div className="reaction-buttons">
                <ReactionsCard
                  reactions={sighting.reactions}
                  currentUserReaction={sighting.currentUserReaction}
                  sightingId={sighting.id}
                />
              </div>
            </div>

            <Row>
              <Col className="species-card d-flex flex-column justify-content-center" sm={5}>
                <Stack direction="horizontal" gap={1} className="align-items-stretch">
                  <div className="border border-2 border-success mb-3" />
                  <Row>
                    <Col xs={6} sm={12} md={6} className="mb-3">
                      <Image src={sighting.species.exampleImageUrl} thumbnail />
                    </Col>
                    <Col xs={6} sm={12} md={6} className="d-flex flex-column justify-content-center mb-3">
                      <h2 className="h5 mb-0 ms-1">Species</h2>
                      <a href={sighting.species.wikiLink} target="_blank" className="ms-1">
                        {sighting.species.name}
                      </a>
                    </Col>
                  </Row>
                </Stack>
              </Col>
              <Col className="sighting-card" sm={7}>
                <Card>
                  <Card.Img
                    variant="top"
                    src={sighting.imageUrl}
                    alt="whale sighting image"
                    style={{ objectFit: "contain", height: "25rem" }}
                  />
                  <Card.Body>
                    <Card.Text>{sighting.description}</Card.Text>
                  </Card.Body>
                </Card>
              </Col>
            </Row>
            <Row>
              <Col className="location-card d-flex flex-column justify-content-center" sm={5}>
                <Stack direction="horizontal" gap={1} className="align-items-stretch">
                  <div className="border border-2 border-success mb-3" />
                  <div>
                    <h2 className="h5 mb-0 mt-4 ms-1">Sight Location</h2>
                    <h3 className="h6 mb-5 ms-1">{sighting.bodyOfWater}</h3>
                  </div>
                </Stack>
              </Col>
              <Col className="map-card" sm={7}>
                <div className="w-100" style={{ height: "25rem" }}>
                  <MapContainer
                    className="h-100"
                    center={[sighting.latitude, sighting.longitude]}
                    zoom={3}
                    scrollWheelZoom={true}
                  >
                    <TileLayer
                      attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                      url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                    />

                    <Marker
                      key={sighting.id}
                      position={[Number(sighting.latitude), Number(sighting.longitude)]}
                      icon={customIcon}
                    >
                      <Popup>{sighting.description}</Popup>
                    </Marker>
                  </MapContainer>
                </div>
              </Col>
            </Row>
          </Stack>
        </Container>
      )}

      {loading && (
        <p>
          Loading...
          <br />
          <Spinner />
        </p>
      )}
      {error && <p>Couldn't load data at this time</p>}
    </>
  )
}

export default SightingView
