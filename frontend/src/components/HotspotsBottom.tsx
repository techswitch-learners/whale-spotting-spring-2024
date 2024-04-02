import { Container, Navbar } from "react-bootstrap"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faCode, faLink } from "@fortawesome/free-solid-svg-icons"
import { faCopyright } from "@fortawesome/free-regular-svg-icons"

const HotspotsBottom = () => {
  return (
    <Navbar fixed="bottom" bg="transparent" data-bs-theme="dark">
      <Container className="justify-content-center">
        <Navbar.Brand className="d-flex flex-column flex-sm-row align-items-center mx-0 fs-6">
          <span>
            <FontAwesomeIcon icon={faCopyright} aria-label="Copyright" /> TechSwitch 2024
          </span>
          <FontAwesomeIcon icon={faCode} aria-hidden className="d-none d-sm-inline-block mx-2" />
          Hotspots data source:
          <FontAwesomeIcon icon={faLink} aria-hidden className="d-none d-sm-inline-block mx-2" />
          <a href="https://wwhandbook.iwc.int/en/responsible-management" target="_blank" className="link-light">
            WHALE WATCHING HANDBOOK
          </a>
        </Navbar.Brand>
      </Container>
    </Navbar>
  )
}

export default HotspotsBottom
