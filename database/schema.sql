CREATE TABLE "rooms" (
  "id" uuid PRIMARY KEY,
  "host_user_id" uuid,
  "code" char(4) UNIQUE NOT NULL
);

CREATE TABLE "users" (
  "room_id" uuid,
  "id" uuid PRIMARY KEY,
  "username" varchar
);

CREATE TABLE "connections" (
  "id" uuid PRIMARY KEY,
  "room_id" uuid,
  "user_id" uuid,
  "connected_at" timestamp,
  "disconnected_at" timestamp
);

CREATE TABLE "rounds" (
  "room_id" uuid,
  "id" uuid PRIMARY KEY,
  "number" int
);

CREATE TABLE "round_submissions" (
  "id" uuid PRIMARY KEY,
  "round_id" uuid,
  "user_id" uuid,
  "score" int,
  "loners" int
);

CREATE TABLE "tables" (
  "round_id" uuid,
  "id" uuid PRIMARY KEY,
  "number" int
);

CREATE TABLE "teams_users" (
  "id" uuid PRIMARY KEY,
  "team_id" uuid,
  "user_id" uuid
);

CREATE TABLE "teams" (
  "table_id" uuid,
  "id" uuid PRIMARY KEY
);

CREATE UNIQUE INDEX ON "rounds" ("room_id", "number");

CREATE UNIQUE INDEX ON "round_submissions" ("round_id", "user_id");

CREATE UNIQUE INDEX ON "tables" ("round_id", "number");

CREATE UNIQUE INDEX ON "teams_users" ("team_id", "user_id");

COMMENT ON TABLE "rounds" IS 'A schedule is defined as a sequence of rounds belonging to a specific room.';

ALTER TABLE "rooms" ADD FOREIGN KEY ("host_user_id") REFERENCES "users" ("id");

ALTER TABLE "users" ADD FOREIGN KEY ("room_id") REFERENCES "rooms" ("id");

ALTER TABLE "connections" ADD FOREIGN KEY ("room_id") REFERENCES "rooms" ("id");

ALTER TABLE "connections" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id");

ALTER TABLE "rounds" ADD FOREIGN KEY ("room_id") REFERENCES "rooms" ("id");

ALTER TABLE "round_submissions" ADD FOREIGN KEY ("round_id") REFERENCES "rounds" ("id");

ALTER TABLE "round_submissions" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id");

ALTER TABLE "tables" ADD FOREIGN KEY ("round_id") REFERENCES "rounds" ("id");

ALTER TABLE "teams_users" ADD FOREIGN KEY ("team_id") REFERENCES "teams" ("id");

ALTER TABLE "teams_users" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id");

ALTER TABLE "teams" ADD FOREIGN KEY ("table_id") REFERENCES "tables" ("id");
