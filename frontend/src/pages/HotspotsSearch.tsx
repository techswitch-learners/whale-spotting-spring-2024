import React, { FormEvent, useState } from "react"
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

function HotspotsSearch() {
  const [searchHotspotsRequest, setSearchHotspotsRequest] = useState<SearchHotspotsRequest>({
    country: "",
    hotspotName: "",
    species: [],
    platforms: [],
    months: [],
  })
  const [hotspots, setHotspots] = useState<Hotspot[]>([])
  const [notFound, setNotFound] = useState<string>()
  const [error, setError] = useState<boolean>(false)

  const fetchHotspotsData = async () => {
    const searchQuery = Object.keys(searchHotspotsRequest)
      .filter((searchKey) => searchHotspotsRequest[searchKey as keyof SearchHotspotsRequest] !== "")
      .map((searchKey) => {
        const searchValues = searchHotspotsRequest[searchKey as keyof SearchHotspotsRequest]
        if (Array.isArray(searchValues)) {
          return searchValues
            .map((eachValue) => `${encodeURIComponent(searchKey)}=${encodeURIComponent(eachValue)}`)
            .join("&")
        } else {
          return `${encodeURIComponent(searchKey)}=${encodeURIComponent(searchValues)}`
        }
      })
      .join("&")

    await fetch(`http://localhost:5280/Hotspots?${searchQuery}`)
      .then((response) => {
        if (response.status == 404) {
          setNotFound("NotFound")
        } else if (response.status == 200) {
          setNotFound("Found")
        }
        return response.json()
      })
      .then((data) => setHotspots(data))
      .catch(() => setError(true))
  }

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

  return (
    <div className="HotspotSearch">
      <h1>Find whale-spotting Hotspots</h1>
      <form onSubmit={handleSubmit}>
        <div className="d-flex justify-content-center">
          <div className="d-flex flex-column justify-content-center align-items-center mx-2">
            <label>
              Town, harbour or region:{" "}
              <input
                type="text"
                name="hotspotName"
                value={searchHotspotsRequest.hotspotName}
                onChange={handleSearchQueryChange}
              />
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
        <br />
        <div className="d-flex justify-content-center">
          <DropdownButton
            id="dropdown-species-button"
            title="Species"
            className="d-flex flex-column justify-content-center align-items-center mx-2"
          >
            {[
              "Beaked whale",
              "Beluga whale",
              "Blue whale",
              "Bowhead whale",
              "Bryde's whale",
              "Fin whale",
              "Gray whale",
              "Humpback whale",
              "Killer whale",
              "Minke whale",
              "Narwhal",
              "Pilot whale",
              "Right whale",
              "Sei whale",
              "Sperm whale",
            ].map((speciesName) => (
              <Dropdown.ItemText>
                <label>
                  <input type="checkbox" name="species" value={speciesName} onChange={handleSearchQueryChange} />{" "}
                  {speciesName}
                </label>
              </Dropdown.ItemText>
            ))}
          </DropdownButton>
          <DropdownButton
            id="dropdown-platforms-button"
            title="Viewing Platforms"
            className="d-flex flex-column justify-content-center align-items-center mx-2"
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
          >
            {[
              "January",
              "February",
              "March",
              "April",
              "May",
              "June",
              "July",
              "August",
              "September",
              "October",
              "November",
              "December",
            ].map((monthName, monthNumber) => (
              <Dropdown.ItemText>
                <label>
                  <input type="checkbox" name="months" value={monthNumber} onChange={handleSearchQueryChange} />{" "}
                  {monthName}
                </label>
              </Dropdown.ItemText>
            ))}
          </DropdownButton>
        </div>
        <button type="submit">Search</button>
      </form>
      <br />
      {!notFound && <h5>Click search to get relevant hotspots ^^</h5>}
      {error && <p>Couldn't load hotspots at this time</p>}
      {notFound == "NotFound" && <h5>Sorry, no such hotspot been found in our database...</h5>}
      <br />
      {notFound == "Found" && (
        <div className="search-results">
          <h5>Found {hotspots.length} relevant hotspots, click the cards for detailed information ^^</h5>
          <ul className="list-unstyled d-flex flex-wrap justify-content-center gap-4">
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
                              {["J", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D"].map(
                                (monthLetter, monthNumber) => (
                                  <div
                                    className={`fw-bold border border-2 border-tertiary d-flex justify-content-center align-items-center 
                                                      ${spot.viewingSuggestions.some((suggestion) => suggestion.months.includes(monthNumber)) ? "bg-tertiary text-white" : ""}`}
                                    style={{ width: "20%", fontSize: "0.5rem" }}
                                  >
                                    {monthLetter}
                                  </div>
                                ),
                              )}
                            </div>
                          </span>
                        </Col>
                        <Col className="d-flex flex-column justify-content-center align-items-center">
                          <Image src={whalesIcon} thumbnail className="border-0" />
                          <span className="fs-3">&times; {spot.viewingSuggestions.length}</span>
                        </Col>
                      </Row>
                    </Card.Body>
                    <Card.Footer></Card.Footer>
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
