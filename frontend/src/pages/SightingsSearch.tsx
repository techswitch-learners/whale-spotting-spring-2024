import "./SightingsSearch.scss"
import { useState, useContext, useEffect, FormEvent } from "react"
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet"
import { Icon } from "leaflet"
import MarkerClusterGroup from "react-leaflet-cluster"
import Form from "react-bootstrap/Form"
import { BackgroundContext } from "../App"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faList, faMapMarker } from "@fortawesome/free-solid-svg-icons"
import Card from "react-bootstrap/Card"
import { Link } from "react-router-dom"
import Sighting from "../models/view/Sighting"
import icon from "/favicon.ico"
import { getSightings, getSpeciesList } from "../api/backendClient"
import { DropdownButton, Dropdown, Row, Col } from "react-bootstrap"
import Species from "../models/view/Species"

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
  const [filteredSightings, setFilteredSightings] = useState<Sighting[]>()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)
  const [speciesList, setSpeciesList] = useState<Species[]>()
  const [startDate, setStartDate] = useState<string>("")
  const [endDate, setEndDate] = useState<string>("")
  const [bodyOfWater, setBodyOfWater] = useState<string>("")
  const [selectedSpeciesSet, setSelectedSpeciesSet] = useState<Set<string>>(new Set<string>())

  useEffect(() => {
    getSpeciesList()
      .then((response) => response.json())
      .then((content) => setSpeciesList(content.speciesList))
      .catch(() => {})
  }, [])

  function getData() {
    setLoading(true)
    setError(false)
    getSightings()
      .then((response) => response.json())
      .then((data) => setAllSightings(data.sightings))
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

  useEffect(getData, [])

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  useEffect(() => {
    setFilteredSightings(allSightings)
  }, [allSightings])

  const handleSubmit = (event: FormEvent) => {
    event.preventDefault()
    setFilteredSightings(
      allSightings?.filter(
        (sighting) =>
          (selectedSpeciesSet.size === 0 || selectedSpeciesSet.has(sighting.species.name)) &&
          (!bodyOfWater || sighting.bodyOfWater.match(new RegExp(bodyOfWater, "i"))) &&
          (!startDate || new Date(sighting.sightingTimestamp) >= new Date(startDate)) &&
          (!endDate || new Date(sighting.sightingTimestamp) <= new Date(endDate)),
      ),
    )
  }

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
          <div>
            <Card className="mx-auto" style={{ maxWidth: "25rem" }}>
              <Card.Body>
                <Form onSubmit={handleSubmit}>
                  <Row className="mb-3">
                    <Form.Group className="col-8" controlId="bodyOfWater">
                      <Form.Label className="text-start">Body of water</Form.Label>
                      <Form.Control
                        type="text"
                        value={bodyOfWater}
                        onChange={(event) => setBodyOfWater(event.target.value)}
                      />
                    </Form.Group>
                    <Col xs={4}>
                      <DropdownButton
                        id="dropdown-species-button"
                        title="Species"
                        className="d-flex flex-column justify-content-center align-items-center mx-2"
                        variant="secondary"
                      >
                        {speciesList?.map((species) => (
                          <Dropdown.ItemText>
                            <Form.Group controlId={`species${species.id}`} className="d-flex">
                              <Form.Check
                                name="species"
                                label={species.name}
                                value={species.name}
                                onChange={(event) => {
                                  if (event.target.checked) {
                                    selectedSpeciesSet.add(event.target.value)
                                  } else {
                                    selectedSpeciesSet.delete(event.target.value)
                                  }
                                  setSelectedSpeciesSet(selectedSpeciesSet)
                                }}
                              />
                            </Form.Group>
                          </Dropdown.ItemText>
                        ))}
                      </DropdownButton>
                    </Col>
                  </Row>
                  <Row className="mb-3">
                    <Form.Group className="col-6" controlId="startDate">
                      <Form.Label className="text-start">Start date</Form.Label>
                      <Form.Control
                        type="date"
                        max={
                          endDate
                            ? new Date(endDate).toISOString().split("T")[0]
                            : new Date().toISOString().split("T")[0]
                        }
                        onChange={(event) => setStartDate(event.target.value)}
                      />
                    </Form.Group>
                    <Form.Group className="col-6" controlId="endDate">
                      <Form.Label className="text-start">End date</Form.Label>
                      <Form.Control
                        type="date"
                        min={startDate ? new Date(startDate).toISOString().split("T")[0] : undefined}
                        max={new Date().toISOString().split("T")[0]}
                        onChange={(event) => setEndDate(event.target.value)}
                      />
                    </Form.Group>
                  </Row>
                  <button type="submit">Filter sightings</button>
                </Form>
              </Card.Body>
            </Card>
            <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
              {filteredSightings?.map((sighting) => (
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
              ))}
            </div>
          </div>
        )}
        {loading && <p>Loading...</p>}
        {error && <p>Sorry, unable to load sightings at this time</p>}
      </div>
    </div>
  )
}

export default SightingsSearch
