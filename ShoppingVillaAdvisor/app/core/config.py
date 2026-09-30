from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    PROJECT_NAME: str = "Shopping Villa Advisor API"
    API_V1_STR: str = "/api/v1"
    DATABASE_URL: str = "sqlite:///./shopping_villa.db"

    class Config:
        env_file = ".env"
        extra = "ignore"
        
settings = Settings()