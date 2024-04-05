import { useState, useContext, useEffect, FormEvent } from "react"
import { DropdownButton, Dropdown, Row, Button, Container } from "react-bootstrap"
import Card from "react-bootstrap/Card"
import Form from "react-bootstrap/Form"
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet"
import MarkerClusterGroup from "react-leaflet-cluster"
import { Link, useSearchParams } from "react-router-dom"
import { faList, faMapMarker } from "@fortawesome/free-solid-svg-icons"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { Icon } from "leaflet"
import { AuthContext, BackgroundContext } from "../App"
import { getSightings, getSpeciesList } from "../api/backendClient"
import ReactionsCard from "../components/ReactionsCard"
import Sighting from "../models/view/Sighting"
import Species from "../models/view/Species"
import "./SightingsSearch.scss"
import icon from "/favicon.ico"

interface SightingCardProps {
  sighting: Sighting
}

function SightingCard({ sighting }: SightingCardProps) {
  return (
    <Card className="SightingCard text-start">
      <Card.Img variant="top" src={sighting.imageUrl} />
      <Card.Body>
        <Card.Title>{sighting.species.name}</Card.Title>
        <Card.Subtitle>{sighting.bodyOfWater}</Card.Subtitle>
        <Card.Text>{sighting.description}</Card.Text>
      </Card.Body>
      <Card.Footer>
        <small>Observed on {sighting.sightingTimestamp.split("T")[0]}</small>
      </Card.Footer>
    </Card>
  )
}

function getFilteredSightings(allSightings: Sighting[], searchParams: URLSearchParams) {
  return allSightings.filter(
    (sighting) =>
      (!searchParams.has("species") || searchParams.has("species", sighting.species.name)) &&
      (!searchParams.has("bodyOfWater") ||
        sighting.bodyOfWater.match(new RegExp(searchParams.get("bodyOfWater")!, "i"))) &&
      (!searchParams.has("startDate") ||
        new Date(sighting.sightingTimestamp) >= new Date(searchParams.get("startDate")!)) &&
      (!searchParams.has("endDate") || new Date(sighting.sightingTimestamp) <= new Date(searchParams.get("endDate")!)),
  )
}

const SightingsSearch = () => {
  const [searchParams, setSearchParams] = useSearchParams()
  const [searchParamsInProgress, setSearchParamsInProgress] = useState<URLSearchParams>(
    new URLSearchParams(searchParams),
  )

  const backgroundContext = useContext(BackgroundContext)

  const [mapView, setMapView] = useState<boolean>(false)
  const [allSightings, setAllSightings] = useState<Sighting[]>()
  const [filteredSightings, setFilteredSightings] = useState<Sighting[]>()
  const [speciesList, setSpeciesList] = useState<Species[]>()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)
  const authContext = useContext(AuthContext)

  useEffect(() => {
    getSpeciesList()
      .then((response) => response.json())
      .then((content) => setSpeciesList(content.speciesList))
      .catch(() => {})
  }, [])

  const getData = () => {
    setLoading(true)
    setError(false)
    getSightings(authContext.cookie.token)
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

  useEffect(getData, [authContext.cookie.token])

  useEffect(() => {
    if (allSightings) {
      setFilteredSightings(getFilteredSightings(allSightings, searchParams))
    }
  }, [allSightings, searchParams])

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  const handleSubmit = (event: FormEvent) => {
    event.preventDefault()
    setSearchParams(new URLSearchParams(searchParamsInProgress))
  }

  return (
    <div className="SightingsSearch text-center">
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
          <>
            <p className="mt-3">Click on the icons to find out more information about the sightings</p>
            <MapContainer center={[26.115, -16.523]} zoom={1} scrollWheelZoom={true} style={{ height: "60vh" }}>
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
          </>
        )}
        {!mapView && (
          <div>
            <Card className="mt-3 mx-auto bg-light" style={{ maxWidth: "25rem" }}>
              <Card.Body>
                <Form onSubmit={handleSubmit}>
                  <div className="d-flex mb-3">
                    <Form.Group
                      className="d-flex flex-column flex-grow-1 justify-content-end me-3"
                      controlId="bodyOfWater"
                    >
                      <Form.Label className="text-start">Body of water</Form.Label>
                      <Form.Control
                        type="text"
                        value={searchParamsInProgress.get("bodyOfWater") ?? ""}
                        onChange={(event) => {
                          if (event.target.value) {
                            searchParamsInProgress.set("bodyOfWater", event.target.value)
                          } else {
                            searchParamsInProgress.delete("bodyOfWater")
                          }
                          setSearchParamsInProgress(new URLSearchParams(searchParamsInProgress))
                        }}
                      />
                    </Form.Group>
                    <div className="d-flex flex-column justify-content-end">
                      <DropdownButton id="dropdown-species-button" title="Species" variant="primary">
                        {speciesList?.map((species) => (
                          <Dropdown.ItemText>
                            <Form.Group controlId={`species${species.id}`}>
                              <Form.Check
                                label={species.name}
                                value={species.name}
                                checked={searchParamsInProgress.has("species", species.name)}
                                onChange={(event) => {
                                  if (event.target.checked) {
                                    searchParamsInProgress.append("species", event.target.value)
                                  } else {
                                    searchParamsInProgress.delete("species", event.target.value)
                                  }
                                  setSearchParamsInProgress(new URLSearchParams(searchParamsInProgress))
                                }}
                              />
                            </Form.Group>
                          </Dropdown.ItemText>
                        ))}
                      </DropdownButton>
                    </div>
                  </div>
                  <Row className="g-3 mb-4 text-start">
                    <Form.Group className="col-12 col-sm-6" controlId="startDate">
                      <Form.Label className="text-start">Start date</Form.Label>
                      <Form.Control
                        type="date"
                        value={searchParamsInProgress.get("startDate") ?? ""}
                        max={
                          searchParamsInProgress.has("endDate")
                            ? new Date(searchParamsInProgress.get("endDate")!).toISOString().split("T")[0]
                            : new Date().toISOString().split("T")[0]
                        }
                        onChange={(event) => {
                          if (event.target.value) {
                            searchParamsInProgress.set("startDate", event.target.value)
                          } else {
                            searchParamsInProgress.delete("startDate")
                          }
                          setSearchParamsInProgress(new URLSearchParams(searchParamsInProgress))
                        }}
                      />
                    </Form.Group>
                    <Form.Group className="col-12 col-sm-6" controlId="endDate">
                      <Form.Label className="text-start">End date</Form.Label>
                      <Form.Control
                        type="date"
                        value={searchParamsInProgress.get("endDate") ?? ""}
                        min={
                          searchParamsInProgress.has("startDate")
                            ? new Date(searchParamsInProgress.get("startDate")!).toISOString().split("T")[0]
                            : undefined
                        }
                        max={new Date().toISOString().split("T")[0]}
                        onChange={(event) => {
                          if (event.target.value) {
                            searchParamsInProgress.set("endDate", event.target.value)
                          } else {
                            searchParamsInProgress.delete("endDate")
                          }
                          setSearchParamsInProgress(new URLSearchParams(searchParamsInProgress))
                        }}
                      />
                    </Form.Group>
                  </Row>
                  <Button type="submit" variant="secondary">
                    Search sightings
                  </Button>
                </Form>
              </Card.Body>
            </Card>
            <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
              {filteredSightings && filteredSightings.length === 0 && <h5>No sightings found</h5>}
              {filteredSightings && filteredSightings.length > 0 && (
                <Container>
                  <ul className="list-unstyled d-flex flex-wrap justify-content-center gap-5 my-4">
                    {filteredSightings.map((sighting) => (
                      <li key={sighting.id}>
                        <ReactionsCard
                          reactions={sighting.reactions}
                          currentUserReaction={sighting.currentUserReaction}
                          sightingId={sighting.id}
                        />
                        <Link
                          to={`/sightings/${sighting.id}`}
                          className="text-decoration-none"
                          style={{ width: "13rem" }}
                        >
                          <SightingCard sighting={sighting} />
                        </Link>
                      </li>
                    ))}
                  </ul>
                </Container>
              )}
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
