import { useContext, useState } from "react"
import { Button, Card, CardBody } from "react-bootstrap"
import { useNavigate } from "react-router-dom"
import { AuthContext } from "../App"
import { addReaction, updateReaction, deleteReaction } from "../api/backendClient"
import Reactions from "../models/view/Reactions"
import ReactionType from "../types/ReactionType"

const emojis = {
  LetsParty: "🤩",
  NiceCatch: "😊",
  SoSo: "😐",
  Suspicious: "🤔",
}
interface ReactionsCardProps {
  reactions: Reactions
  currentUserReaction: ReactionType | null
  sightingId: number
}

const ReactionsCard = ({ reactions, currentUserReaction, sightingId }: ReactionsCardProps) => {
  const authContext = useContext(AuthContext)
  const navigate = useNavigate()

  const [reactionResponse, setReactionResponse] = useState<ReactionsCardProps>({
    reactions: reactions,
    currentUserReaction: currentUserReaction,
    sightingId: sightingId,
  })
  const [, setErrors] = useState<{ [subject: string]: string[] }>({})

  const deleteUserReaction = () => {
    deleteReaction(sightingId, authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => setReactionResponse(data))
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        } else {
          response.json().then((content) => {
            setErrors(content.errors)
          })
        }
      })
      .catch(() => setErrors({ General: ["Unable to delete your reaction"] }))
  }

  const updateUserReaction = (type: ReactionType) => {
    updateReaction({ type, sightingId }, authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => {
            setReactionResponse(data)
          })
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        } else {
          response.json().then((content) => {
            setErrors(content.errors)
          })
        }
      })
      .catch(() => setErrors({ General: ["Unable to update reaction"] }))
  }

  const addUserReaction = (type: ReactionType) => {
    addReaction({ type, sightingId }, authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => setReactionResponse(data))
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        } else {
          response.json().then((content) => {
            setErrors(content.errors)
          })
        }
      })
      .catch(() => setErrors({ General: ["Unable to add reaction"] }))
  }

  const handleClick = (type: ReactionType) => {
    if (reactionResponse.currentUserReaction === type) {
      deleteUserReaction()
    } else if (type !== reactionResponse.currentUserReaction && reactionResponse.currentUserReaction !== null) {
      updateUserReaction(type)
    } else {
      addUserReaction(type)
    }
  }

  return (
    <>
      <Card>
        <CardBody className="d-flex justify-content-between p-2">
          {Object.entries(emojis).map(([key, emoji]) => (
            <div key={key} className="d-flex flex-column mx-1">
              {key === reactionResponse.currentUserReaction && (
                <Button
                  variant="warning"
                  className="border border-warning"
                  onClick={() => handleClick(key as ReactionType)}
                >
                  {emoji}
                </Button>
              )}
              {key !== reactionResponse.currentUserReaction && (
                <Button variant="light" className="border" onClick={() => handleClick(key as ReactionType)}>
                  {emoji}
                </Button>
              )}
              <span>{reactionResponse.reactions[key as ReactionType] || "0"}</span>
            </div>
          ))}
        </CardBody>
      </Card>
    </>
  )
}

export default ReactionsCard
