import { FormEvent, useContext, useEffect, useState } from "react"
import { Card, Col, Row, Image, Form, Button, Container } from "react-bootstrap"
import Dropdown from "react-bootstrap/Dropdown"
import DropdownButton from "react-bootstrap/DropdownButton"
import { Link, useSearchParams } from "react-router-dom"
import { faCalendar } from "@fortawesome/free-regular-svg-icons"
import { faHelicopter, faPersonHiking, faPersonSwimming, faSailboat } from "@fortawesome/free-solid-svg-icons"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { BackgroundContext } from "../App"
import { getHotspots, getMonths, getSpeciesList } from "../api/backendClient"
import whalesIcon from "../assets/whales.png"
import Hotspot from "../models/view/Hotspot"
import Species from "../models/view/Species"
import "./HotspotsSearch.scss"

function HotspotsSearch() {
  const [searchParams, setSearchParams] = useSearchParams()
  const [searchParamsInProgress, setSearchParamsInProgress] = useState<URLSearchParams>(
    new URLSearchParams(searchParams),
  )

  const backgroundContext = useContext(BackgroundContext)

  const [hotspots, setHotspots] = useState<Hotspot[]>()
  const [speciesList, setSpeciesList] = useState<Species[]>()
  const [monthList, setMonthList] = useState<string[]>()
  const [error, setError] = useState<boolean>(false)

  const handleSubmit = (event: FormEvent) => {
    event.preventDefault()
    setSearchParams(new URLSearchParams(searchParamsInProgress))
  }

  useEffect(() => {
    getSpeciesList()
      .then((response) => response.json())
      .then((content) => setSpeciesList(content.speciesList))
      .catch(() => {})

    getMonths()
      .then((response) => response.json())
      .then((content) => setMonthList(content))
      .catch(() => {})
  }, [])

  useEffect(() => {
    getHotspots(searchParams.toString())
      .then((response) => response.json())
      .then((data) => setHotspots(data))
      .catch(() => setError(true))
  }, [searchParams])

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  return (
    <div className="HotspotsSearch text-center h-100">
      <h1>Find whale-spotting hotspots</h1>
      <p className="mb-3">
        Data courtesy of the{" "}
        <a href="https://wwhandbook.iwc.int/en/responsible-management" target="_blank">
          Whale Watching Handbook
        </a>
      </p>
      <Card className="mx-auto bg-light" style={{ maxWidth: "25rem" }}>
        <Card.Body>
          <Form onSubmit={handleSubmit}>
            <Row className="g-3 mb-3">
              <Form.Group className="col-12 col-sm-7 text-start" controlId="name">
                <Form.Label>Town, harbour or region</Form.Label>
                <Form.Control
                  type="text"
                  value={searchParamsInProgress.get("name") ?? ""}
                  onChange={(event) => {
                    if (event.target.value) {
                      searchParamsInProgress.set("name", event.target.value)
                    } else {
                      searchParamsInProgress.delete("name")
                    }
                    setSearchParamsInProgress(new URLSearchParams(searchParamsInProgress))
                  }}
                />
              </Form.Group>
              <Form.Group className="col-12 col-sm-5 text-start" controlId="country">
                <Form.Label>Country</Form.Label>
                <Form.Control
                  type="text"
                  value={searchParamsInProgress.get("country") ?? ""}
                  onChange={(event) => {
                    if (event.target.value) {
                      searchParamsInProgress.set("country", event.target.value)
                    } else {
                      searchParamsInProgress.delete("country")
                    }
                    setSearchParamsInProgress(new URLSearchParams(searchParamsInProgress))
                  }}
                />
              </Form.Group>
            </Row>
            <div className="mb-4 d-flex flex-wrap justify-content-center gap-2">
              <DropdownButton id="dropdown-months-button" title="Months" variant="primary">
                {monthList?.map((monthName, monthNumber) => (
                  <Dropdown.ItemText>
                    <Form.Group controlId={`month${monthNumber}`}>
                      <Form.Check
                        label={monthName}
                        value={monthName}
                        checked={searchParamsInProgress.has("months", monthName)}
                        onChange={(event) => {
                          if (event.target.checked) {
                            searchParamsInProgress.append("months", event.target.value)
                          } else {
                            searchParamsInProgress.delete("months", event.target.value)
                          }
                          setSearchParamsInProgress(new URLSearchParams(searchParamsInProgress))
                        }}
                      />
                    </Form.Group>
                  </Dropdown.ItemText>
                ))}
              </DropdownButton>
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
              <DropdownButton id="dropdown-platforms-button" title="Viewing options" variant="primary">
                {["Boat-based", "Swimming", "Land-based", "Aerial"].map((platformName, platformNumber) => (
                  <Dropdown.ItemText>
                    <Form.Group controlId={`platform${platformNumber}`}>
                      <Form.Check
                        label={platformName}
                        value={platformNumber}
                        checked={searchParamsInProgress.has("platforms", platformNumber.toString())}
                        onChange={(event) => {
                          if (event.target.checked) {
                            searchParamsInProgress.append("platforms", event.target.value)
                          } else {
                            searchParamsInProgress.delete("platforms", event.target.value)
                          }
                          setSearchParamsInProgress(new URLSearchParams(searchParamsInProgress))
                        }}
                      />
                    </Form.Group>
                  </Dropdown.ItemText>
                ))}
              </DropdownButton>
            </div>
            <Button type="submit" variant="secondary">
              Search hotspots
            </Button>
          </Form>
        </Card.Body>
      </Card>
      <div className="d-flex justify-content-center my-3">
        {error && <h5>Couldn't load hotspots at this time</h5>}
        {hotspots && hotspots.length === 0 && <h5>No hotspots found</h5>}
      </div>
      {hotspots && hotspots.length > 0 && (
        <Container>
          <h5 className="mb-4">{hotspots.length} hotspots found</h5>
          <ul className="list-unstyled d-flex flex-wrap justify-content-center gap-4 mb-0">
            {hotspots
              .sort((hotspot1, hotspot2) => hotspot1.name.localeCompare(hotspot2.name))
              .map((spot) => (
                <li key={spot.id}>
                  <Link to={`http://localhost:5173/Hotspots/${spot.id}`} className="text-decoration-none">
                    <Card className="HotspotCard" style={{ width: "20rem" }}>
                      <Card.Header className="d-flex flex-column justify-content-between" style={{ height: "7.5rem" }}>
                        <Card.Title>{spot.name}</Card.Title>
                        <Card.Subtitle className="text-end">{spot.country}</Card.Subtitle>
                      </Card.Header>
                      <Card.Body>
                        <Row>
                          <Col className="border-end">
                            <p className="mb-2">Possible months</p>
                            <span className="position-relative d-inline-block">
                              <FontAwesomeIcon
                                icon={faCalendar}
                                className="text-tertiary"
                                style={{ fontSize: "7.5rem" }}
                              />
                              <div
                                className="position-absolute d-flex flex-wrap justify-content-center gap-1"
                                style={{ top: "3rem", left: "1rem", right: "1rem", bottom: "1rem" }}
                              >
                                {monthList?.map((monthLetter, monthNumber) => (
                                  <div
                                    className={`fw-bold border border-2 border-tertiary d-flex justify-content-center align-items-center 
                                                      ${spot.viewingSuggestions.some((suggestion) => suggestion.months.includes(monthNumber)) ? "bg-tertiary text-white" : ""}`}
                                    style={{ width: "20%", fontSize: "0.5rem" }}
                                  >
                                    {monthLetter[0]}
                                  </div>
                                ))}
                              </div>
                            </span>
                          </Col>
                          <Col className="d-flex flex-column justify-content-center align-items-center">
                            <p className="mb-2">Possible species</p>
                            <Image src={whalesIcon} alt="three cartoon whales" thumbnail className="border-0" />
                            <span className="fs-3">&times; {spot.viewingSuggestions.length}</span>
                          </Col>
                        </Row>
                      </Card.Body>
                      <Card.Footer>
                        <p className="mb-2">Possible viewing options</p>
                        <Row className="d-flex justify-content-start align-items-center mx-3">
                          <Col className="d-flex justify-content-center align-items-center">
                            <FontAwesomeIcon
                              icon={faSailboat}
                              title="Boat-based"
                              className={
                                spot.viewingSuggestions.some((suggestion) => suggestion.platformBoxes.includes(0))
                                  ? "text-secondary"
                                  : "opacity-50"
                              }
                            />
                          </Col>
                          <Col className="d-flex justify-content-center align-items-center">
                            <FontAwesomeIcon
                              icon={faPersonSwimming}
                              title="Swimming"
                              className={`fs-5 ${spot.viewingSuggestions.some((suggestion) => suggestion.platformBoxes.includes(1)) ? "text-secondary" : "opacity-50"}`}
                            />
                          </Col>
                          <Col className="d-flex justify-content-center align-items-center">
                            <FontAwesomeIcon
                              icon={faPersonHiking}
                              title="Land-based"
                              className={`fs-5 ${spot.viewingSuggestions.some((suggestion) => suggestion.platformBoxes.includes(2)) ? "text-secondary" : "opacity-50"}`}
                            />
                          </Col>
                          <Col className="d-flex justify-content-center align-items-center">
                            <FontAwesomeIcon
                              icon={faHelicopter}
                              title="Aerial"
                              className={
                                spot.viewingSuggestions.some((suggestion) => suggestion.platformBoxes.includes(3))
                                  ? "text-secondary"
                                  : "opacity-50"
                              }
                            />
                          </Col>
                        </Row>
                      </Card.Footer>
                    </Card>
                  </Link>
                </li>
              ))}
          </ul>
        </Container>
      )}
    </div>
  )
}

export default HotspotsSearch
