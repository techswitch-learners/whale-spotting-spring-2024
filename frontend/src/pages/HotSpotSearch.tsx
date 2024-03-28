import React, { FormEvent, useState } from "react"
import HotSpot from "../models/view/HotSpot"
import { Link } from "react-router-dom"
import Dropdown from "react-bootstrap/Dropdown"
import DropdownButton from "react-bootstrap/DropdownButton"
import { Card, Col, Row, Image } from "react-bootstrap"
import "./HotSpotSearch.scss"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faCalendar } from "@fortawesome/free-regular-svg-icons"
import whalesIcon from "../assets/whales.png"

interface SearchParams {
  Country: string
  HotSpotName: string
  Species: string[]
  Platforms: number[]
  Months: number[]
}

function HotSpotSearch() {
  const [hotSpots, setHotSpots] = useState<HotSpot[]>([])
  const [searchParams, setSearchParams] = useState<SearchParams>({
    Country: "",
    HotSpotName: "",
    Species: [],
    Platforms: [],
    Months: [],
  })

  const fetchData = async () => {
    try {
      const queryParams = Object.keys(searchParams)
        .filter((key) => searchParams[key as keyof SearchParams] !== "")
        .map((key) => {
          const value = searchParams[key as keyof SearchParams]
          if (Array.isArray(value)) {
            return value.map((val) => `${encodeURIComponent(key)}=${encodeURIComponent(val)}`).join("&")
          } else {
            return `${encodeURIComponent(key)}=${encodeURIComponent(value)}`
          }
        })
        .join("&")
      const response = await fetch(`http://localhost:5280/hotspots?${queryParams}`)
      const responseData = await response.json()
      setHotSpots(responseData)
    } catch (error) {
      console.error("Error fetching data:", error)
    }
  }

  const handleSubmit = (event: FormEvent) => {
    event.preventDefault()
    fetchData()
  }

  const handleSearchParamsChange = (
    event: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLSelectElement>,
  ) => {
    const { name, value } = event.target
    setSearchParams((prevParams) => ({
      ...prevParams,
      [name]: value,
    }))
  }

  return (
    <div className="HotSpotSearch">
      <h1>Find whale-spotting hotspots</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>
            Town, harbour or region:{" "}
            <input
              type="text"
              name="HotSpotName"
              value={searchParams.HotSpotName}
              onChange={handleSearchParamsChange}
            />
          </label>
        </div>
        <div>
          <label>
            Country:{" "}
            <input type="text" name="Country" value={searchParams.Country} onChange={handleSearchParamsChange} />
          </label>
        </div>
        <div>
          <label>
            Species 1:{" "}
            <input type="text" name="Species" value={searchParams.Species} onChange={handleSearchParamsChange} />
          </label>
          <label>
            Species 2:{" "}
            <input type="text" name="Species" value={searchParams.Species[1]} onChange={handleSearchParamsChange} />
          </label>
        </div>
        <DropdownButton id="dropdown-item-button" title="Viewing Platforms">
          <Dropdown.ItemText>
            <label>
              <input type="checkbox" name="Platforms" value={0} onChange={handleSearchParamsChange} /> Boat-based
            </label>
          </Dropdown.ItemText>
          <Dropdown.ItemText>
            <label>
              <input type="checkbox" name="Platforms" value={1} onChange={handleSearchParamsChange} /> Swimming
            </label>
          </Dropdown.ItemText>
          <Dropdown.ItemText>
            <label>
              <input type="checkbox" name="Platforms" value={2} onChange={handleSearchParamsChange} /> Land-based
            </label>
          </Dropdown.ItemText>
          <Dropdown.ItemText>
            <label>
              <input type="checkbox" name="Platforms" value={3} onChange={handleSearchParamsChange} /> Aerial
            </label>
          </Dropdown.ItemText>
        </DropdownButton>

        <button type="submit">Search</button>
      </form>

      <ul className="list-unstyled d-flex flex-wrap justify-content-center gap-4">
        {hotSpots.map((spot) => (
          <li key={spot.id}>
            <Link to={`http://localhost:5173/hotspots/${spot.id}`} className="text-decoration-none">
              <Card style={{ width: "18rem" }}>
                <Card.Header className="d-flex flex-column justify-content-between" style={{ height: "7.5rem" }}>
                  <Card.Title>{spot.name}</Card.Title>
                  <Card.Subtitle className="text-end">{spot.country}</Card.Subtitle>
                </Card.Header>
                <Card.Body>
                  <Row>
                    <Col>
                      <span className="position-relative d-inline-block">
                        <FontAwesomeIcon icon={faCalendar} className="text-tertiary" style={{ fontSize: "7.5rem" }} />
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
  )
}

export default HotSpotSearch
