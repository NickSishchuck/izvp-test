# Project Startup Guide

## Prerequisites

**Install Docker Desktop for Windows:**
- Watch: https://www.youtube.com/watch?v=WEO3gt7NVKI
- Verify installation: Open terminal and run `docker --version`

## Quick Start

### 1. Clone Repository
```bash
git clone https://github.com/NickSishchuck/izvp-test.git
cd izvp-test
```

### 2. Checkout Your Branch
```bash
git checkout dev/{yourname}
```

### 3. Start Application
```bash
docker-compose up --build
```

Wait for logs to show:
```
backend    | Starting Kestrel on http://0.0.0.0:5000
frontend   | serving on port 80
```

### 4. Access Application
- **Frontend:** http://localhost:8080
- **Frontend admin:** http://localhost:8080/admin.html
- **Backend API:** http://localhost:5000/api/
- **Swagger:** http://localhost:5000

### 5. Stop Application
Press `Ctrl+C` in terminal, then:
```bash
docker-compose down
```

## Troubleshooting

**Changes not showing:**
```bash
docker-compose down
docker-compose up --build
```

**Backend not responding:**
- Check logs: `docker-compose logs backend`
- Verify backend is healthy: http://localhost:5000/api/health

**Need fresh start:**
```bash
docker-compose down -v
docker-compose up --build
```
