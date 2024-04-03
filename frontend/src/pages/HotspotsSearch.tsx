import React, { FormEvent, useEffect, useState } from "react"
import { Link } from "react-router-dom"
import Dropdown from "react-bootstrap/Dropdown"
import DropdownButton from "react-bootstrap/DropdownButton"
import { Card, Col, Row, Image } from "react-bootstrap"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faCalendar } from "@fortawesome/free-regular-svg-icons"
import whalesIcon from "../assets/whales.png"
import Hotspot from "../models/view/Hotspot"
import SearchHotspotsRequest from "../models/request/SearchHotspotsRequest"
import "./HotspotsSearch.scss"
import { getHotspots, getMonthList, getSpeciesList } from "../api/backendClient"
import { faHelicopter, faPersonHiking, faPersonSwimming, faSailboat } from "@fortawesome/free-solid-svg-icons"
import Species from "../models/view/Species"

function HotspotsSearch() {
  // const [searchParams] = useSearchParams();
  const [searchHotspotsRequest, setSearchHotspotsRequest] = useState<SearchHotspotsRequest>({
    country: "",
    name: "",
    species: [],
    platforms: [],
    months: [],
  })
  const [hotspots, setHotspots] = useState<Hotspot[]>()
  const [error, setError] = useState<boolean>(false)
  const [speciesList, setSpeciesList] = useState<Species[]>()
  const [monthList, setMonthList] = useState<string[]>()

  useEffect(() => {
    getSpeciesList()
      .then((response) => response.json())
      .then((content) => setSpeciesList(content.speciesList))
      .catch(() => {})
  }, [])

  useEffect(() => {
    getMonthList()
      .then((response) => response.json())
      .then((content) => setMonthList(content))
      .catch(() => {})
  }, [])

  const handleSubmit = (event: FormEvent) => {
    event.preventDefault()
    fetchHotspotsData()
  }

  const handleSearchQueryChange = (
    event: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLSelectElement>,
  ) => {
    const { name, value } = event.target
    setSearchHotspotsRequest((prevParams) => ({
      ...prevParams,
      [name]: value,
    }))
  }

  const fetchHotspotsData = () => {
    const searchParams = new URLSearchParams()
    Object.keys(searchHotspotsRequest).forEach((searchKey) => {
      const searchValues = searchHotspotsRequest[searchKey as keyof SearchHotspotsRequest]
      if (Array.isArray(searchValues)) {
        searchValues.forEach((eachValue) => {
          searchParams.append(searchKey, eachValue.toString())
        })
      } else {
        searchParams.set(searchKey, searchValues)
      }
    })

    getHotspots(searchParams.toString())
      .then((response) => response.json())
      .then((data) => setHotspots(data))
      .catch(() => setError(true))
  }

  return (
    <div className="HotspotsSearch">
      <h1>Find whale-spotting hotspots</h1>
      <p>
        Data courtesy of the{" "}
        <a href="https://wwhandbook.iwc.int/en/responsible-management" target="_blank">
          Whale Watching Handbook
        </a>
      </p>
      <form onSubmit={handleSubmit}>
        <div className="d-flex justify-content-center my-3">
          <div className="d-flex flex-column justify-content-center align-items-center mx-2">
            <label>
              Town, harbour or region:{" "}
              <input type="text" name="name" value={searchHotspotsRequest.name} onChange={handleSearchQueryChange} />
            </label>
          </div>
          <div className="d-flex flex-column justify-content-center align-items-center mx-2">
            <label>
              Country:{" "}
              <input
                type="text"
                name="country"
                value={searchHotspotsRequest.country}
                onChange={handleSearchQueryChange}
              />
            </label>
          </div>
        </div>
        <div className="d-flex justify-content-center my-3">
          <DropdownButton
            id="dropdown-species-button"
            title="Species"
            className="d-flex flex-column justify-content-center align-items-center mx-2"
            variant="secondary"
          >
            {speciesList?.map((species) => (
              <Dropdown.ItemText>
                <label>
                  <input type="checkbox" name="species" value={species.name} onChange={handleSearchQueryChange} />{" "}
                  {species.name}
                </label>
              </Dropdown.ItemText>
            ))}
          </DropdownButton>
          <DropdownButton
            id="dropdown-platforms-button"
            title="Viewing Platforms"
            className="d-flex flex-column justify-content-center align-items-center mx-2"
            variant="secondary"
          >
            {["Boat-based", "Swimming", "Land-based", "Aerial"].map((platformName, platformNumber) => (
              <Dropdown.ItemText>
                <label>
                  <input type="checkbox" name="platforms" value={platformNumber} onChange={handleSearchQueryChange} />{" "}
                  {platformName}
                </label>
              </Dropdown.ItemText>
            ))}
          </DropdownButton>
          <DropdownButton
            id="dropdown-months-button"
            title="Time of Year"
            className="d-flex flex-column justify-content-center align-items-center mx-2"
            variant="secondary"
          >
            {monthList?.map((monthName, monthNumber) => (
              <Dropdown.ItemText>
                <label>
                  <input type="checkbox" name="months" value={monthNumber} onChange={handleSearchQueryChange} />{" "}
                  {monthName}
                </label>
              </Dropdown.ItemText>
            ))}
          </DropdownButton>
        </div>
        <div className="d-flex justify-content-center my-3">
          <button type="submit">Search</button>
        </div>
      </form>
      <div className="d-flex justify-content-center my-3">
        {error && <h5>Couldn't load hotspots at this time</h5>}
        {hotspots && hotspots.length === 0 && (
          <h5>Sorry, no such hotspot been found in our database...try other search criteria</h5>
        )}
      </div>
      {hotspots && hotspots.length > 0 && (
        <div className="search-results">
          <h5>
            {hotspots.length} hotspots of your interests have been found, click the cards for more information to
            prepare your trip ^^
          </h5>
          <ul className="list-unstyled d-flex flex-wrap justify-content-center gap-4 my-4">
            {hotspots.map((spot) => (
              <li key={spot.id}>
                <Link to={`http://localhost:5173/Hotspots/${spot.id}`} className="text-decoration-none">
                  <Card style={{ width: "18rem" }}>
                    <Card.Header className="d-flex flex-column justify-content-between" style={{ height: "7.5rem" }}>
                      <Card.Title>{spot.name}</Card.Title>
                      <Card.Subtitle className="text-end">{spot.country}</Card.Subtitle>
                    </Card.Header>
                    <Card.Body>
                      <Row>
                        <Col>
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
                          <Image src={whalesIcon} thumbnail className="border-0" />
                          <span className="fs-3">&times; {spot.viewingSuggestions.length}</span>
                        </Col>
                      </Row>
                    </Card.Body>
                    <Card.Footer>
                      <Row className="d-flex justify-content-start align-items-center mx-3">
                        <Col className="d-flex align-items-center">
                          <FontAwesomeIcon
                            icon={faSailboat}
                            className={
                              spot.viewingSuggestions.some((suggestion) => suggestion.platformBoxes.includes(0))
                                ? "text-secondary"
                                : "opacity-50"
                            }
                          />
                        </Col>
                        <Col className="d-flex align-items-center">
                          <FontAwesomeIcon
                            icon={faPersonSwimming}
                            className={`fs-5 ${spot.viewingSuggestions.some((suggestion) => suggestion.platformBoxes.includes(1)) ? "text-secondary" : "opacity-50"}`}
                          />
                        </Col>
                        <Col className="d-flex align-items-center">
                          <FontAwesomeIcon
                            icon={faPersonHiking}
                            className={`fs-5 ${spot.viewingSuggestions.some((suggestion) => suggestion.platformBoxes.includes(2)) ? "text-secondary" : "opacity-50"}`}
                          />
                        </Col>
                        <Col className="d-flex align-items-center">
                          <FontAwesomeIcon
                            icon={faHelicopter}
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
        </div>
      )}
    </div>
  )
}

export default HotspotsSearch
