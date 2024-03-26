import { useContext, useState, useEffect } from "react"
import { useParams } from "react-router-dom"
import { Container, Card } from "react-bootstrap"
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
        <Container className="pb-4" style={{ width: "20rem" }}>
          <Card className="text-start" id="main-sighting-image">
            <Card.Img variant="top" src={sighting?.imageUrl} />
          </Card>
          <Card className="text-start" id="species-card">
            <Card.Body style={{ display: "flex", gap: "1rem" }}>
              <div style={{ backgroundColor: "green", height: "10rem", width: "0.5rem" }}></div>
              <div>
                <Card.Img variant="right" style={{ height: "10rem" }} src={sighting?.species.exampleImageUrl} />
                <Card.Title>
                  <Card.Title>Species</Card.Title>
                  <Card.Link href={sighting?.species.wikiLink} target="_blank">
                    {sighting?.species.name}
                  </Card.Link>
                </Card.Title>
              </div>
            </Card.Body>
          </Card>
          <Card className="text-start" id="map-card">
            <div style={{ height: "20rem", width: "100%" }}>
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
          </Card>
        </Container>
      )}
      {loading && <p>Loading...</p>}
      {error && <p>Error fetching sighting from the backend</p>}
    </>
  )
}

export default SightingView
