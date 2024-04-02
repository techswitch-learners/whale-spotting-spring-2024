interface AdminUser {
  id: number
  userName: string
  email: string | null
  phoneNumber: string | null
  lockOutEnd: string
  lockoutEnabled: boolean
  accessFailedCount: number
}

export default AdminUser
