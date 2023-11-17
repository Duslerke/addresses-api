SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;
SET default_tablespace = '';
SET default_with_oids = false;

CREATE TABLE public.hackney_address (
    "lpi_key" varchar(14) NOT NULL,
    "lpi_logical_status" varchar(18),
    "lpi_start_date" int4 NOT NULL,
    "lpi_end_date" int4 NOT NULL,
    "lpi_last_update_date" int4 NOT NULL,
    "usrn" int4,
    "uprn" int8 NOT NULL,
    "parent_uprn" int8,
    "blpu_start_date" int4 NOT NULL,
    "blpu_end_date" int4 NOT NULL,
    "blpu_class" varchar(4),
    "blpu_last_update_date" int4 NOT NULL,
    "usage_description" varchar(160),
    "usage_primary" varchar(160),
    "property_shell" bool NOT NULL,
    "easting" float8 NOT NULL,
    "northing" float8 NOT NULL,
    "unit_number" int4,
    "sao_text" varchar(90),
    "building_number" varchar(17),
    "pao_text" varchar(90),
    "paon_start_num" int4,
    "street_description" varchar(100),
    "locality" varchar(100),
    "ward" varchar(100),
    "town" varchar(100),
    "postcode" varchar(8),
    "postcode_nospace" varchar(8),
    "planning_use_class" varchar(50),
    "neverexport" bool NOT NULL,
    "longitude" float8 NOT NULL,
    "latitude" float8 NOT NULL,
    "gazetteer" varchar(8),
    "organisation" varchar(100),
    "line1" varchar(200),
    "line2" varchar(200),
    "line3" varchar(200),
    "line4" varchar(100),
    PRIMARY KEY ("lpi_key")
);
ALTER TABLE public.hackney_address OWNER TO postgresuser;
