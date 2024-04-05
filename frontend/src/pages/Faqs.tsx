import { useState, useEffect, useContext } from "react"
import { Image, ListGroup, Table } from "react-bootstrap"
import Accordion from "react-bootstrap/Accordion"
import { Link } from "react-router-dom"
import { BackgroundContext } from "../App"
import { getAchievements } from "../api/backendClient"
import Achievement from "../models/view/Achievement"
import "./Faqs.scss"

const Faqs = () => {
  const backgroundContext = useContext(BackgroundContext)

  const [achievements, setAchievements] = useState<Array<Achievement>>()
  const [, setLoading] = useState<boolean>(false)
  const [, setError] = useState<boolean>(false)

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  useEffect(() => {
    setLoading(true)
    setError(false)
    getAchievements()
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => {
            console.log(data)
            setAchievements(data)
          })
        } else if (response.status === 404) {
          console.log("Page not found")
        } else {
          setError(true)
        }
      })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }, [])

  return (
    <>
      <h1>Questions</h1>
      <Accordion flush>
        <Accordion.Item eventKey="0">
          <Accordion.Header>Q1. How can I get experience?</Accordion.Header>
          <Accordion.Body className="align-left">
            You can gain experience by adding whale sighting reports. Every time you add a sighting report, you earn 50
            experience points (exp). To upload your sighting, please use the following link:
            <Link to={"/sightings/add"}>Upload Your Sighting</Link> Additionally, if your sighting report receives
            reactions from other viewers, you can earn extra experience points. Here's how the reactions translate into
            experience:
            <ListGroup className="align-left">
              <ListGroup.Item>🤩 : +10 exp</ListGroup.Item>
              <ListGroup.Item>😊 : +6 exp</ListGroup.Item>
              <ListGroup.Item>😐 : +3 exp</ListGroup.Item>
              <ListGroup.Item>🤔 : +0 exp (But we value your insight!)</ListGroup.Item>
            </ListGroup>
            Keep spotting and sharing your experiences to grow as a whale spotter!
          </Accordion.Body>
        </Accordion.Item>

        <Accordion.Item eventKey="1">
          <Accordion.Header>Q2. How can I get badges?</Accordion.Header>
          <Accordion.Body className="align-left">
            Badges are a way to showcase your progress and achievement levels in our whale spotting community. You gain
            experience points (exp) by adding sighting reports and receiving reactions. As you accumulate experience,
            you get promoted to new achievement levels, each with its unique badge. Here's a brief overview of the
            achievement levels and the minimum experience needed for each:
            <Table className="align-left" striped bordered hover>
              <thead>
                <tr>
                  <th>Badge</th>
                  <th>Achievement Name</th>
                  <th>Description</th>
                  <th>Min Experience</th>
                </tr>
              </thead>
              <tbody>
                {achievements &&
                  achievements?.map((achievement) => (
                    <tr key={achievement.name}>
                      <td>
                        <Image
                          style={{ height: "100px", width: "100px" }}
                          src={`${import.meta.env.BASE_URL}achievement-badges-images/${achievement.badgeImageUrl}`}
                          alt={`Badge image of ${achievement.name}`}
                          roundedCircle
                        />
                      </td>
                      <td>{achievement.name}</td>
                      <td>{achievement.description}</td>
                      <td>{achievement.minExperience}</td>
                    </tr>
                  ))}
              </tbody>
            </Table>
          </Accordion.Body>
        </Accordion.Item>
      </Accordion>
    </>
  )
}

export default Faqs
