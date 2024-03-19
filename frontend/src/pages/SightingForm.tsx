import { useContext, useState, FormEventHandler } from "react"
import { Navigate } from "react-router-dom"
import { Button, CardText, Form, Spinner } from "react-bootstrap"
import Row from "react-bootstrap/Row"
import Col from "react-bootstrap/Col"
import { AuthContext } from "../App"

//    description: string
// imageUrl: string
// bodyOfWaterId?: number
const SightingForm = () => {
  const authContext = useContext(AuthContext)
  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<string>()
  const [latitude, setLatitude] = useState<number>()
  const [longitude, setLongitude] = useState<number>()
  const [speciesId, setSpeciesId] = useState<number>()
  const [sightingDate, setSightingDate] = useState<Date>(new Date())
  const [description, setDescription] = useState<string>()
  const [imgUrl, setImageUrl] = useState<string>()
  const [bodyOfWaterId, setBodyOfWaterId] = useState<number>()

  //const [addSightingRequest, setAddSightingRequest] = useState<AddSightingRequest>()

  // var formData = new FormData(event.target as HTMLFormElement);
  const addSighting: FormEventHandler = (event) => {
    event.preventDefault()
    setLoading(true)
    setError(undefined)
    const addSighting = {
      latitude: latitude,
      longitude: longitude,
      token: authContext.cookie.token,
      speciesId: speciesId,
      description: description,
      imageUrl: imgUrl,
      bodyOfWaterId: bodyOfWaterId,
      sightingTimeStamp: sightingDate,
    }
    console.log(addSighting)
    // setAddSightingRequest(addSighting)
  }

  if (!authContext.cookie.token) {
    return <Navigate to="/login" />
  }

  return (
    <>
      <Form onSubmit={addSighting}>
        <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingLatitude">
          <Form.Label column sm={2} className="mb-1">
            Latitude
          </Form.Label>
          <Col sm={5}>
            <Form.Control
              type="text"
              value={latitude}
              onChange={(event) => {
                setLatitude(Number(event.target.value))
                setError(undefined)
              }}
            />
          </Col>
        </Form.Group>

        <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingLongitude">
          <Form.Label column sm={2} className="mb-1">
            Longitude
          </Form.Label>
          <Col sm={5}>
            <Form.Control
              type="text"
              value={longitude}
              onChange={(event) => {
                setLongitude(Number(event.target.value))
                setError(undefined)
              }}
            />
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
                setError(undefined)
              }}
            >
              <option>Select the body of water</option>
              <option value={1}>Arctic</option>
              <option value={2}>North Atlantic</option>
              <option value={3}>South Atlantic</option>
              <option value={4}>North Pacific</option>
              <option value={5}>South Pacific</option>
            </Form.Select>
          </Col>
        </Form.Group>

        <Form.Group as={Row} className="mb-3 text-start" controlId="formSightingSightingDate">
          <Form.Label column sm={2} className="mb-1">
            Date
          </Form.Label>
          <Col sm={5}>
            <Form.Control
              type="date"
              max-value={new Date().getDate()}
              onChange={(event) => {
                setSightingDate(new Date(event.target.value))
                setError(undefined)
              }}
            />
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
                setError(undefined)
              }}
            >
              <option>Select the Species of the whale</option>
              <option value={1}>Beluga Whale</option>
              <option value={2}>Right Whale</option>
              <option value={3}>Fin Whale</option>
            </Form.Select>
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
                setError(undefined)
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
              onChange={(event) => {
                setDescription(event.target.value)
                setError(undefined)
              }}
            />
          </Col>
        </Form.Group>

        <Button variant="primary" type="submit" disabled={loading}>
          {loading ? <Spinner variant="light" size="sm" /> : <>Submit</>}
        </Button>
        {error && <CardText className="text-danger mt-2">{error}</CardText>}
      </Form>
    </>
  )
}

export default SightingForm
