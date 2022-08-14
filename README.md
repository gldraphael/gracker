# Gracker

Gracker is a google-analytics-like web tracker.   
The main goal of this project, at the moment, is to give me ideas to blog about.


## Quickstart

```sh
# setup dev dependencies
cd dev
docker compose pull
docker compose up -d
cd ..

# run Gracker
docker compose build
docker compose up
```

This will make Gracker available on the following URLs:

Port       | URL/Port
----------:|---------
Worker App | http://localhost:17701
REST API   | http://localhost:17702
Admin App  | http://localhost:17703
