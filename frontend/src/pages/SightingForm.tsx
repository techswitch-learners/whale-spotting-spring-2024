import { useContext, useState, FormEventHandler, useEffect } from "react"
import { Link, Navigate } from "react-router-dom"
import { Button, CardText, Form, Spinner } from "react-bootstrap"
import Row from "react-bootstrap/Row"
import Col from "react-bootstrap/Col"
import { AuthContext, BackgroundContext } from "../App"
import { addSighting } from "../api/backendClient"
import ErrorList from "../components/ErrorList"

const SightingForm = () => {
  const backgroundContext = useContext(BackgroundContext)

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  const authContext = useContext(AuthContext)
  const [latitude, setLatitude] = useState<number>()
  const [longitude, setLongitude] = useState<number>()
  const [speciesId, setSpeciesId] = useState<number>()
  const [sightingDate, setSightingDate] = useState<Date>(new Date())
  const [description, setDescription] = useState<string>()
  const [imageUrl, setImageUrl] = useState<string>()
  const [bodyOfWaterId, setBodyOfWaterId] = useState<number>()

  const [loading, setLoading] = useState<boolean>(false)
  const [success, setSuccess] = useState<boolean>(false)
  const [errors, setErrors] = useState<{ [subject: string]: string[] }>({})

  const submitSighting: FormEventHandler = (event) => {
    event.preventDefault()
    setLoading(true)
    setSuccess(false)
    setErrors({})

    addSighting({
      latitude: latitude,
      longitude: longitude,
      token: authContext.cookie.token,
      speciesId: speciesId,
      description: description,
      imageUrl: imageUrl,
      bodyOfWaterId: bodyOfWaterId,
      sightingTimeStamp: sightingDate,
    })
      .then((response) => {
        if (response.ok) {
          setLatitude(undefined)
          setLongitude(undefined)
          setSpeciesId(undefined)
          setSightingDate(new Date())
          setDescription(undefined)
          setImageUrl(undefined)
          setBodyOfWaterId(undefined)
          setSuccess(true)
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
    <>
      <Form onSubmit={submitSighting}>
        <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingLatitude">
          <Form.Label column sm={2} className="mb-1">
            Latitude
          </Form.Label>
          <Col sm={5}>
            <Form.Control
              type="number"
              value={latitude}
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
          <Form.Label column sm={2} className="mb-1">
            Longitude
          </Form.Label>
          <Col sm={5}>
            <Form.Control
              type="number"
              value={longitude}
              onChange={(event) => {
                setLongitude(Number(event.target.value))
                setSuccess(false)
                setErrors({})
              }}
            />
            <ErrorList errors={errors["Longitude"]} />
          </Col>
        </Form.Group>

        <Form.Group as={Row} className="mb-3" controlId="formSightingBodyOfWaterId">
          <Form.Label column sm={2} className="mb-1">
            Body of Water{" "}
          </Form.Label>
          <Col sm={5}>
            <Form.Select
              aria-label="formSightingBodyOfWaterId"
              onChange={(event) => {
                setBodyOfWaterId(Number(event.target.value))
                setSuccess(false)
                setErrors({})
              }}
            >
              <option>Select the body of water</option>
              <option value={1}>Arctic</option>
              <option value={2}>North Atlantic</option>
              <option value={3}>South Atlantic</option>
              <option value={4}>North Pacific</option>
              <option value={5}>South Pacific</option>
            </Form.Select>
            <ErrorList errors={errors["BodyOfWaterId"]} />
          </Col>
        </Form.Group>

        <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingSightingDate">
          <Form.Label column sm={2} className="mb-1">
            Date
          </Form.Label>
          <Col sm={5}>
            <Form.Control
              type="date"
              max={new Date().toISOString().split("T")[0]}
              onChange={(event) => {
                setSightingDate(new Date(event.target.value))
                setSuccess(false)
                setErrors({})
              }}
            />
            <ErrorList errors={errors["Date"]} />
          </Col>
        </Form.Group>

        <Form.Group as={Row} className="mb-3" controlId="formSightingSpeciesId">
          <Form.Label column sm={2} className="mb-1">
            SpeciesId
          </Form.Label>
          <Col sm={5}>
            <Form.Select
              aria-label="SpeciesId"
              onChange={(event) => {
                setSpeciesId(Number(event.target.value))
                setSuccess(false)
                setErrors({})
              }}
            >
              <option>Select the Species of the whale</option>
              <option value={1}>Beluga Whale</option>
              <option value={2}>Right Whale</option>
              <option value={3}>Fin Whale</option>
            </Form.Select>
            <ErrorList errors={errors["SpeciesId"]} />
          </Col>
        </Form.Group>

        <Form.Group as={Row} controlId="formSightingImageUrl" className="mb-3">
          <Form.Label column sm={2}>
            Upload Image
          </Form.Label>
          <Col sm={5}>
            <Form.Control
              type="url"
              onChange={(event) => {
                setImageUrl(event.target.value)
                setSuccess(false)
                setErrors({})
              }}
            />
          </Col>
        </Form.Group>

        <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingDescription">
          <Form.Label column sm={2} className="mb-1">
            Description
          </Form.Label>
          <Col sm={5}>
            <Form.Control
              as="textarea"
              value={description}
              max-length="140"
              onChange={(event) => {
                setDescription(event.target.value)
                setSuccess(false)
                setErrors({})
              }}
            />
          </Col>
        </Form.Group>

        <Button variant="primary" type="submit" disabled={loading}>
          {loading ? <Spinner variant="light" size="sm" /> : <>Submit</>}
        </Button>

        {success && (
          <CardText className="text-success mt-2 mb-0">
            Add sighting successful!
            <br />
            You can now go to{" "}
            <Link to="/sighting/all" className="link-success">
              All sightings
            </Link>
            .
          </CardText>
        )}
        <ErrorList errors={errors["General"]} />
      </Form>
    </>
  )
}

export default SightingForm
