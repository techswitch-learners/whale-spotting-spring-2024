import { useContext, useEffect } from "react"
import { BackgroundContext } from "../App"
import whaleAnimation from "../assets/whale-animation.gif"
import eye from "../assets/eye.svg"
import { Button, Col, Row } from "react-bootstrap"
import { LinkContainer } from "react-router-bootstrap"
import NavbarBottom from "../components/NavbarBottom"

const Home = () => {
  const backgroundContext = useContext(BackgroundContext)

  useEffect(() => {
    backgroundContext.setBackground(`url("${whaleAnimation}")`)
  }, [backgroundContext])

  return (
    <>
      <Row className="Home flex-grow-1">
        <Col lg={6} className="d-flex flex-column justify-content-center pe-lg-5">
          <h1 className="display-3 text-center text-white" aria-label="Welcome Whale Spotters!">
            Welcome
            <br />
            Whale
            <br />
            Sp
            <img src={eye} id="eyeId" alt="Eye taking the place of letter O" />
            tters!
          </h1>
        </Col>
        <Col lg={6} className="d-flex flex-column justify-content-center">
          <div className="Home-button-container d-flex flex-column justify-content-center align-items-stretch gap-3">
            <LinkContainer to="/sightings/add">
              <Button variant="translucent" size="lg">
                Add a whale sighting
              </Button>
            </LinkContainer>
            <LinkContainer to="/sightings">
              <Button variant="translucent" size="lg">
                See whale sightings
              </Button>
            </LinkContainer>
            <LinkContainer to="/hotspots">
              <Button variant="translucent" size="lg">
                Find whale-spotting hotspots
              </Button>
            </LinkContainer>
          </div>
          <div className="Home-button-container d-flex flex-column justify-content-center align-items-stretch gap-3">
            <LinkContainer to="/sightings/add">
              <Button variant="translucent">Add a whale sighting</Button>
            </LinkContainer>
            <LinkContainer to="/sightings">
              <Button variant="translucent">See whale sightings</Button>
            </LinkContainer>
            <LinkContainer to="/hotspots">
              <Button variant="translucent">Find whale-spotting hotspots</Button>
            </LinkContainer>
          </div>
        </Col>
      </Row>
      <NavbarBottom />
    </>
  )
}
export default Home
