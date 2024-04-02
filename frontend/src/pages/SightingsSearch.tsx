import "./SightingsSearch.scss"
import { useState, useContext, useEffect } from "react"
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet"
import { Icon } from "leaflet"
import MarkerClusterGroup from "react-leaflet-cluster"
import Form from "react-bootstrap/Form"
import { AuthContext, BackgroundContext } from "../App"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faList, faMapMarker } from "@fortawesome/free-solid-svg-icons"
import Card from "react-bootstrap/Card"
import { Link } from "react-router-dom"
import Sighting from "../models/view/Sighting"
import icon from "/favicon.ico"
import { getSightings } from "../api/backendClient"
import Reactions from "../components/Reactions"
import { Stack } from "react-bootstrap"

interface SightingCardProps {
  imageUrl: string
  species: string
  bodyOfWater: string
  description: string
  sightingTimestamp: string
}

function SightingCard({ imageUrl, species, bodyOfWater, description, sightingTimestamp }: SightingCardProps) {
  return (
    <Card className="text-start">
      <Card.Img variant="top" src={imageUrl} />
      <Card.Body>
        <Card.Title>{species}</Card.Title>
        <Card.Subtitle>{bodyOfWater}</Card.Subtitle>
        <Card.Text>{description}</Card.Text>
      </Card.Body>
      <Card.Footer>
        <small>{sightingTimestamp}</small>
      </Card.Footer>
    </Card>
  )
}

const SightingsSearch = () => {
  const backgroundContext = useContext(BackgroundContext)
  const [mapView, setMapView] = useState<boolean>(false)
  const [allSightings, setAllSightings] = useState<Sighting[]>()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)
  const authContext = useContext(AuthContext)

  function getData() {
    setLoading(true)
    setError(false)
    getSightings(authContext.cookie.token)
      .then((response) => response.json())
      .then((data) => {
        setAllSightings(data.sightings)
        console.log(data.sightings)
      })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  const customIcon = new Icon({
    iconUrl: icon,
    iconSize: [24, 24],
  })

  const toggleView = () => {
    setMapView(!mapView)
  }

  useEffect(getData, [authContext.cookie.token])

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  return (
    <div className="SightingsSearch d-flex flex-column text-center">
      <h1>Whale Sightings</h1>
      <div className="d-flex flex-column">
        <Form className="d-flex align-items-center align-self-center position-relative">
          <Form.Check
            type="switch"
            role="button"
            id="layout-switch"
            label={
              <>
                <FontAwesomeIcon icon={faList} className="pe-none" />
                <FontAwesomeIcon icon={faMapMarker} className="pe-none" />
              </>
            }
            onClick={toggleView}
          />
          <Form.Label htmlFor="layout-switch" className="ms-3 mb-0">
            {mapView ? "Map View" : "List View"}
          </Form.Label>
        </Form>
        {mapView && (
          <div className="mapContainer">
            <p>Click on the icon to see more details about the whale sighting</p>
            <MapContainer center={[26.115, -16.523]} zoom={1} scrollWheelZoom={true}>
              <TileLayer
                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
              />
              <MarkerClusterGroup>
                {allSightings?.map((sighting) => (
                  <Marker
                    key={sighting.id}
                    position={[Number(sighting.latitude), Number(sighting.longitude)]}
                    icon={customIcon}
                  >
                    <Popup>{sighting.description}</Popup>
                  </Marker>
                ))}
              </MarkerClusterGroup>
            </MapContainer>
          </div>
        )}
        {!mapView && (
          <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
            {allSightings?.map((sighting) => (
              <Stack key={sighting.id}>
                <Reactions
                  reactions={sighting.reactions}
                  currentUserReaction={sighting.currentUserReaction}
                  sightingId={sighting.id}
                />
                <Link
                  to={`/sightings/${sighting.id}`}
                  key={sighting.id}
                  className="text-decoration-none"
                  style={{ width: "13rem" }}
                >
                  <SightingCard
                    imageUrl={sighting.imageUrl}
                    species={sighting.species.name}
                    bodyOfWater={sighting.bodyOfWater}
                    description={sighting.description}
                    sightingTimestamp={"Observed on " + sighting.sightingTimestamp.split("T")[0]}
                  />
                </Link>
              </Stack>
            ))}
          </div>
        )}
        {loading && <p>Loading...</p>}
        {error && <p>Sorry, unable to load sightings at this time</p>}
      </div>
    </div>
  )
}

export default SightingsSearch
