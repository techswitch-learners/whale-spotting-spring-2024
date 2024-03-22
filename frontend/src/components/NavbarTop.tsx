import { useContext } from "react"
import { LinkContainer } from "react-router-bootstrap"
import { Container, Nav, Navbar } from "react-bootstrap"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import {
  faHouse,
  faCircleInfo,
  faCircleQuestion,
  faUserPen,
  faUserLock,
  faUserPlus,
  IconDefinition,
} from "@fortawesome/free-solid-svg-icons"
import { AuthContext } from "../App"

interface NavbarLinkProps {
  to: string
  text: string
  icon: IconDefinition
}

const NavbarLink = ({ to, text, icon }: NavbarLinkProps) => {
  return (
    <LinkContainer to={to}>
      <Nav.Link
        active={false}
        className="d-inline-flex flex-lg-row-reverse align-items-center justify-content-end ms-2 p-0"
      >
        {text}
        <FontAwesomeIcon icon={icon} className="ms-2 me-lg-2" style={{ width: "1.25rem" }} />
      </Nav.Link>
    </LinkContainer>
  )
}

const NavbarTop = () => {
  const authContext = useContext(AuthContext)

  return (
    <Navbar fixed="top" expand="lg" collapseOnSelect bg="tertiary" data-bs-theme="dark">
      <Container fluid className="text-center">
        <Navbar.Brand>Whale Spotting</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="gap-2 w-100 mt-2 mb-1 my-lg-0">
            <NavbarLink to="/" text="Home" icon={faHouse} />
            <NavbarLink to="/sightings" text="Sightings" icon={faCircleQuestion} />
            <NavbarLink to="/about" text="About" icon={faCircleInfo} />
            <NavbarLink to="/faqs" text="FAQs" icon={faCircleQuestion} />

            <hr className="ms-lg-auto my-0 border-light" />
            {authContext.cookie.token ? (
              <>
                <NavbarLink to="/profile" text="Profile" icon={faUserPen} />
                <NavbarLink to="/logout" text="Logout" icon={faUserLock} />
              </>
            ) : (
              <>
                <NavbarLink to="/login" text="Log in" icon={faUserLock} />
                <NavbarLink to="/register" text="Register" icon={faUserPlus} />
              </>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  )
}

export default NavbarTop
