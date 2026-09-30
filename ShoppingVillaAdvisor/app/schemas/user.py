from pydantic import BaseModel, EmailStr

class UserCreate(BaseModel):
    id: int
    username: str
    email: EmailStr
    password: str


class UserResponse(BaseModel):
    id: int

    model_config = {
        "from_attributes": True
    }