import "./AllSightings.scss"
import { useState, useContext, useEffect } from "react"
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet"
import "leaflet/dist/leaflet.css"
import { Icon } from "leaflet"
import MarkerClusterGroup from "react-leaflet-cluster"
import Form from "react-bootstrap/Form"
import { BackgroundContext } from "../../App"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faList, faMapMarker } from "@fortawesome/free-solid-svg-icons"
import Card from "react-bootstrap/Card"
import Col from "react-bootstrap/Col"
import Row from "react-bootstrap/Row"

interface SightingsCardProps {
  imgUrl: string
  species: string
  bodyOfWater: string
  description: string
  sightingTimestamp: string
}

function SightingsCard({ imgUrl, species, bodyOfWater, description, sightingTimestamp }: SightingsCardProps) {
  const [clicked, setClicked] = useState(false)

  const handleClick = () => {
    setClicked(!clicked)
  }

  return (
    <Card>
      <Card.Img variant="top" src={imgUrl} />
      <Card.Body>
        <Card.Title>{species}</Card.Title>
        <Card.Subtitle>{bodyOfWater}</Card.Subtitle>
        <Card.Text onClick={handleClick} className={clicked ? "show" : " "}>
          {description}
        </Card.Text>
      </Card.Body>
      <Card.Footer>
        <small className="-mtextuted">{sightingTimestamp}</small>
      </Card.Footer>
    </Card>
  )
}

const AllSightings = () => {
  const backgroundContext = useContext(BackgroundContext)
  const [mapView, setMapView] = useState<boolean>(false)
  const [allSightings, setAllSightings] = useState<SightingsData>()
  const [error, setErrror] = useState(false)

  useEffect(() => {
    backgroundContext.setBackground("white")
    getData()
  }, [backgroundContext])

  interface SightingsData {
    sightings: {
      id: number
      latitude: number
      longitude: number
      species: {
        name: string
      }
      description: string
      bodyOfWaterName: string
      imageUrl: string
      sightingTimestamp: string
    }[]
  }

  function getData() {
    fetch("http://localhost:5280/sightings")
      .then((response) => response.json())
      .then((data) => setAllSightings(data))
      .catch(() => setErrror(true))
  }

  const customIcon = new Icon({
    iconUrl: "favicon.ico",
    iconSize: [25, 25],
  })

  const toggleView = () => {
    setMapView(!mapView)
  }

  return (
    <div className="mainContainer">
      <h1>Whale Sightings</h1>
      <div className="contentContainer">
        <Form className="layoutToggleForm">
          <Form.Check
            type="switch"
            id="layout-switch"
            label={
              <>
                <FontAwesomeIcon icon={faList} />
                <FontAwesomeIcon icon={faMapMarker} />
              </>
            }
            onClick={toggleView}
          />
          <Form.Label htmlFor="layout-switch">{mapView ? "Map View" : "List View"}</Form.Label>
        </Form>
        {mapView && (
          <div className="mapContainer">
            <p>
              Use the zoom button to see the exact location of the whale sighting <br /> Click on the icon to see more
              details about the whale sighting
            </p>
            <MapContainer center={[26.115, -16.523]} zoom={1} scrollWheelZoom={true}>
              <TileLayer
                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
              />
              <MarkerClusterGroup>
                {allSightings?.sightings.map((sighting) => (
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
          <div className="sightingsContainer">
            <Row xs={1} md={3} lg={4} className="g-4">
              {allSightings?.sightings.map((sighting) => (
                <Col key={sighting.id}>
                  <SightingsCard
                    imgUrl={sighting.imageUrl}
                    species={sighting.species.name}
                    bodyOfWater={sighting.bodyOfWaterName}
                    description={sighting.description}
                    sightingTimestamp={"Observed on " + sighting.sightingTimestamp.split("T")[0]}
                  />
                </Col>
              ))}
            </Row>
          </div>
        )}
        {error && <p>There was an error</p>}
      </div>
    </div>
  )
}

export default AllSightings
