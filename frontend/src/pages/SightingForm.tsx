import { useContext, useState, FormEventHandler, useEffect } from "react"
import { useNavigate, Link, Navigate } from "react-router-dom"
import { Button, CardText, Form, Spinner, Row, Col, Card, CardBody } from "react-bootstrap"
import { AuthContext, BackgroundContext } from "../App"
import { addSighting, getBodiesOfWater, getSpeciesList } from "../api/backendClient"
import ErrorList from "../components/ErrorList"
import BodyOfWater from "../models/view/BodyOfWater"
import Species from "../models/view/Species"
import { MapContainer, TileLayer, useMapEvents, Marker } from "react-leaflet"
import "leaflet/dist/leaflet.css"

interface GetLocationProps {
  setLatitude: (latitude: number) => void
  setLongitude: (longitude: number) => void
}

function GetLocation({ setLatitude, setLongitude }: GetLocationProps) {
  useMapEvents({
    click: (event) => {
      setLatitude(event.latlng.lat)
      setLongitude(event.latlng.lng)
    },
  })
  return null
}

const SightingForm = () => {
  const navigate = useNavigate()

  const backgroundContext = useContext(BackgroundContext)
  const authContext = useContext(AuthContext)
  const defaultLat = 51.505
  const defaultLong = -0.09

  const [showMap, setShowMap] = useState(false)

  const [bodiesOfWater, setBodiesOfWater] = useState<BodyOfWater[]>()
  const [speciesList, setSpeciesList] = useState<Species[]>()

  const [latitude, setLatitude] = useState<number>(defaultLat)
  const [longitude, setLongitude] = useState<number>(defaultLong)

  const [speciesId, setSpeciesId] = useState<number | null>(null)
  const [sightingDate, setSightingDate] = useState<Date | null>(null)
  const [description, setDescription] = useState<string>("")
  const [imageUrl, setImageUrl] = useState<string>("")
  const [bodyOfWaterId, setBodyOfWaterId] = useState<number | null>(null)

  const [loading, setLoading] = useState<boolean>(false)
  const [success, setSuccess] = useState<boolean>(false)
  const [errors, setErrors] = useState<{ [subject: string]: string[] }>({})

  const toggleMap = () => {
    setShowMap(!showMap)
  }

  useEffect(() => {
    getBodiesOfWater()
      .then((response) => response.json())
      .then((content) => setBodiesOfWater(content.bodiesOfWater))
      .catch(() => {})

    getSpeciesList()
      .then((response) => response.json())
      .then((content) => setSpeciesList(content.speciesList))
      .catch(() => {})
  }, [])

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  const submitSighting: FormEventHandler = (event) => {
    event.preventDefault()
    setLoading(true)
    setSuccess(false)
    setErrors({})

    addSighting(
      {
        latitude: latitude,
        longitude: longitude,
        speciesId: speciesId,
        description: description,
        imageUrl: imageUrl,
        bodyOfWaterId: bodyOfWaterId,
        sightingTimeStamp: sightingDate,
      },
      authContext.cookie.token,
    )
      .then((response) => {
        if (response.ok) {
          setLatitude(defaultLat)
          setLongitude(defaultLong)
          setDescription("")
          setImageUrl("")
          const form = event.target as HTMLFormElement
          form.reset()
          setSuccess(true)
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        } else {
          response.json().then((content) => {
            setErrors(content.errors)
          })
        }
      })
      .catch(() => setErrors({ General: ["Unable to submit your sighting. Please try again later."] }))
      .finally(() => setLoading(false))
  }

  if (!authContext.cookie.token) {
    return <Navigate to="/login" />
  }

  return (
    <div className="text-center">
      <h1>Add a Sighting</h1>
      <p>Use the map to find the coordinates of the sighting location or enter them manually</p>
      <Card className="d-flex align-items-center border-0">
        <CardBody className="w-100">
          <Form onSubmit={submitSighting}>
            <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingLatitude">
              <Form.Label column md={4} className="text-md-end">
                Latitude
              </Form.Label>
              <Col md={5}>
                <Form.Control
                  type="number"
                  value={latitude ?? undefined}
                  onChange={(event) => {
                    setLatitude(Number(event.target.value))
                    setSuccess(false)
                    setErrors({})
                  }}
                />
                <ErrorList errors={errors["Latitude"]} />
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingLongitude">
              <Form.Label column md={4} className="text-md-end">
                Longitude
              </Form.Label>
              <Col md={5}>
                <Form.Control
                  type="number"
                  value={longitude ?? undefined}
                  onChange={(event) => {
                    setLongitude(Number(event.target.value))
                    setSuccess(false)
                    setErrors({})
                  }}
                />
                <ErrorList errors={errors["Longitude"]} />
              </Col>
            </Form.Group>
            <Form.Group className="mb-3">
              <Button className="mb-3" onClick={toggleMap}>
                {showMap ? "Hide Map" : "Show Map"}
              </Button>
              {showMap && (
                <MapContainer
                  center={[defaultLat, defaultLong]}
                  zoom={5}
                  className="mw-100 mx-auto"
                  style={{ width: "37.5rem", height: "44vh" }}
                >
                  <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                  />
                  <GetLocation setLatitude={setLatitude} setLongitude={setLongitude} />
                  <Marker position={[latitude, longitude]}></Marker>
                </MapContainer>
              )}
            </Form.Group>
            <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingBodyOfWaterId">
              <Form.Label column md={4} className="text-md-end">
                Body of water
              </Form.Label>
              <Col md={5}>
                <Form.Select
                  aria-label="formSightingBodyOfWaterId"
                  onChange={(event) => {
                    setBodyOfWaterId(Number(event.target.value))
                    setSuccess(false)
                    setErrors({})
                  }}
                >
                  <option>Select the body of water</option>
                  {bodiesOfWater?.map((bodyOfWater) => (
                    <option key={bodyOfWater.id} value={bodyOfWater.id}>
                      {bodyOfWater.name}
                    </option>
                  ))}
                </Form.Select>
                <ErrorList errors={errors["BodyOfWaterId"]} />
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingSightingDate">
              <Form.Label column md={4} className="text-md-end">
                Date
              </Form.Label>
              <Col md={5}>
                <Form.Control
                  type="date"
                  max={new Date().toISOString().split("T")[0]}
                  onChange={(event) => {
                    setSightingDate(new Date(event.target.value))
                    setSuccess(false)
                    setErrors({})
                  }}
                />
                <ErrorList errors={errors["SightingTimestamp"]} />
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingSpeciesId">
              <Form.Label column md={4} className="text-md-end">
                Species
              </Form.Label>
              <Col md={5}>
                <Form.Select
                  aria-label="SpeciesId"
                  onChange={(event) => {
                    setSpeciesId(Number(event.target.value))
                    setSuccess(false)
                    setErrors({})
                  }}
                >
                  <option>Select the species</option>
                  {speciesList?.map((species) => (
                    <option key={species.id} value={species.id}>
                      {species.name}
                    </option>
                  ))}
                </Form.Select>
                <ErrorList errors={errors["SpeciesId"]} />
              </Col>
            </Form.Group>

            <Form.Group as={Row} controlId="formSightingImageUrl" className="mb-3 text-start">
              <Form.Label column md={4} className="text-md-end">
                Upload image
              </Form.Label>
              <Col md={5}>
                <Form.Control
                  type="url"
                  onChange={(event) => {
                    setImageUrl(event.target.value)
                    setSuccess(false)
                    setErrors({})
                  }}
                />
                <ErrorList errors={errors["ImageUrl"]} />
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingDescription">
              <Form.Label column md={4} className="text-md-end">
                Description
              </Form.Label>
              <Col md={5}>
                <Form.Control
                  as="textarea"
                  value={description ?? ""}
                  max-length="140"
                  onChange={(event) => {
                    setDescription(event.target.value)
                    setSuccess(false)
                    setErrors({})
                  }}
                />
                <ErrorList errors={errors["Description"]} />
              </Col>
            </Form.Group>

            <Button variant="primary" type="submit" disabled={loading}>
              {loading ? <Spinner variant="light" size="sm" /> : <>Submit</>}
            </Button>

            {success && (
              <CardText className="text-success mt-2 mb-0">
                Add sighting successful!
                <br />
                You can view other sightings on the{" "}
                <Link to="/sightings/" className="link-success">
                  sightings page
                </Link>{" "}
                while you are waiting for your sighting to be approved.
              </CardText>
            )}
            <ErrorList errors={errors["General"]} />
          </Form>
        </CardBody>
      </Card>
    </div>
  )
}

export default SightingForm
