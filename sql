--
-- PostgreSQL database dump
--

-- Dumped from database version 10.11
-- Dumped by pg_dump version 10.11

-- Started on 2020-01-04 14:36:53

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

--
-- TOC entry 196 (class 1259 OID 16439)
-- Name: tovary; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tovary (
    uid uuid NOT NULL,
    name text,
    code text,
    description text,
    "Field1" text,
    "Field2" text,
    "Field3" text,
    "Field4" text,
    "Field5" text
);


ALTER TABLE public.tovary OWNER TO postgres;

--
-- TOC entry 2792 (class 0 OID 16439)
-- Dependencies: 196
-- Data for Name: tovary; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tovary (uid, name, code, description, "Field1", "Field2", "Field3", "Field4", "Field5") FROM stdin;
99fe2078-398a-4e7b-b285-e0e966fe5d59	Test	\N	\N	\N	\N	\N	\N	\N
e6b298cc-e364-463b-ac86-6a542826961c	Test	\N	\N	\N	\N	\N	\N	\N
97a0655c-91b1-4cb7-be9c-ffc4f595cf45	Test	\N	\N	\N	\N	\N	\N	\N
eed172b9-9f0a-412c-9599-70b4ac90a421	Test	\N	\N	\N	\N	\N	\N	\N
8f424da6-57da-4b3d-801b-2ccc3b4e5cb2	Test	001	desc	\N	\N	\N	\N	\N
a6993ec9-0e36-4e55-8cc8-e94421977c39	Test	001	desc	\N	\N	\N	\N	\N
ecdc259b-c220-47fb-899f-d86328217395	Test	001	desc	\N	\N	\N	\N	\N
16e36d9c-0318-4c0f-9f37-da707f0543be	Test	001	desc	\N	\N	\N	\N	\N
b346ccfc-0a17-455b-a833-f60a6670c924	Test	001	desc	\N	\N	\N	\N	\N
780ebf1a-8b95-4852-9ea3-f21962e97034	Test	001	desc	\N	\N	\N	\N	\N
37dfba24-5e2b-4a01-81df-c91f21ddd9ac	Name 0	001	desc	\N	\N	\N	\N	\N
dd1f3004-680e-42e2-8b0a-9444ba53a3fb	Name 1	001	desc	\N	\N	\N	\N	\N
b315363a-62ae-4f62-bb43-93635f2f2f23	Name 2	001	desc	\N	\N	\N	\N	\N
88171944-ccb9-4d49-9cea-b0dbbdd7cd00	Name 3	001	desc	\N	\N	\N	\N	\N
cb150859-8e83-428f-973d-60b2452dfe8d	Name 4	001	desc	\N	\N	\N	\N	\N
98353d77-a1b8-4bb4-b78d-b403bfbcb71a	Name 5	001	desc	\N	\N	\N	\N	\N
39a7eff4-d1d8-48a9-895b-7eb4555f2c22	Name 6	001	desc	\N	\N	\N	\N	\N
d0e118f8-5bdb-4896-8516-ba8f1b78366e	Name 7	001	desc	\N	\N	\N	\N	\N
4e3cce1c-df39-444d-a32c-80a3ec0cca04	Name 8	001	desc	\N	\N	\N	\N	\N
2ef24376-770f-496b-be2a-7ee1bca2631e	Name 9	001	desc	\N	\N	\N	\N	\N
a60a7fe4-fe59-4492-bff6-611b0271c1b6	Name 0	001	desc	\N	\N	\N	\N	\N
b6e01f69-d1e6-4177-9086-492838ea50ad	Name 1	001	desc	\N	\N	\N	\N	\N
7429b2ce-966d-4d3b-9818-b119e9616204	Name 2	001	desc	\N	\N	\N	\N	\N
357093c2-68db-45ba-b26a-b5a371c2e7b8	Name 3	001	desc	\N	\N	\N	\N	\N
81917791-f6d1-43a1-ae5a-66cf3db41665	Name 4	001	desc	\N	\N	\N	\N	\N
adb9c70b-ff29-4175-bdd4-1ffec03f19f9	Name 5	001	desc	\N	\N	\N	\N	\N
b4ed0969-a24a-4349-888d-7eff83c9c5a4	Name 6	001	desc	\N	\N	\N	\N	\N
c51e0125-3b35-4a43-841a-936aa759d254	Name 7	001	desc	\N	\N	\N	\N	\N
b6a1844d-5991-4ca9-9fca-c29a14cf5a30	Name 8	001	desc	\N	\N	\N	\N	\N
f393a0bb-7ca8-443c-b99a-b174b73a6ab5	Name 9	001	desc	\N	\N	\N	\N	\N
7a0bdcb3-4905-4a80-978f-24a79215db2b	Name 0	001	desc	\N	\N	\N	\N	\N
4958bc5c-b3dc-43a1-bb9d-247ace7e6bfc	Name 1	001	desc	\N	\N	\N	\N	\N
96463302-6c4b-4fb1-be46-ab4cc29d8305	Name 2	001	desc	\N	\N	\N	\N	\N
b9c13ac1-a40c-4375-a86c-8827a1f308d7	Name 3	001	desc	\N	\N	\N	\N	\N
95e7f5e8-5ca1-4d02-9a8e-8478d5c050d5	Name 4	001	desc	\N	\N	\N	\N	\N
3c075f53-2a83-42af-b528-bf73b9da7ef1	Name 5	001	desc	\N	\N	\N	\N	\N
f8db4d7c-1995-4eda-a8c4-0fc13b306f0f	Name 6	001	desc	\N	\N	\N	\N	\N
c7dbbcbe-eaa1-4ce5-85f9-7426c41b356f	Name 7	001	desc	\N	\N	\N	\N	\N
92c04e48-29ba-4a9f-be3c-6a6d817459f4	Name 8	001	desc	\N	\N	\N	\N	\N
c4ef136f-2f0f-461e-aa31-b4722a994ec7	Name 9	001	desc	\N	\N	\N	\N	\N
38800806-8717-4eb3-92a5-d1ccd977ca6c	Name 0	001	desc	\N	\N	\N	\N	\N
7bfa64be-0fc7-45db-b90b-e6614d8e66b7	Name 1	001	desc	\N	\N	\N	\N	\N
63a6a458-fb2f-4b9e-88dc-6d722ee3d942	Name 2	001	desc	\N	\N	\N	\N	\N
f956e2ca-ab3e-493a-b8b6-6ce7599fef8a	Name 3	001	desc	\N	\N	\N	\N	\N
ec79ebcc-f98d-46d1-b9c3-5d60f895037d	Name 4	001	desc	\N	\N	\N	\N	\N
ed64f917-6d95-4c85-8e11-0c2d1c134df1	Name 5	001	desc	\N	\N	\N	\N	\N
599ca9c9-a05c-40c9-9aad-8711ec3b09f5	Name 6	001	desc	\N	\N	\N	\N	\N
473b9a63-3998-4e56-bd59-67b2baed81b0	Name 7	001	desc	\N	\N	\N	\N	\N
7c413e27-ec80-49fb-9749-08e37857bfd0	Name 8	001	desc	\N	\N	\N	\N	\N
4c2975f3-e7bb-40ca-8849-2d631c0352e4	Name 9	001	desc	\N	\N	\N	\N	\N
c6fcca7d-eaa7-46e5-8701-fb2c082faf75	Name 0	001	desc	\N	\N	\N	\N	\N
afb7cd68-e52e-4a99-8e94-43fa66b1c8b5	Name 1	001	desc	\N	\N	\N	\N	\N
8f20b16b-be1a-45a8-9ec8-9f7006e284ca	Name 2	001	desc	\N	\N	\N	\N	\N
f45d3250-f93f-4913-9114-6d6960c8b0cf	Name 3	001	desc	\N	\N	\N	\N	\N
7dda2e79-b383-4df6-8feb-95106748b255	Name 4	001	desc	\N	\N	\N	\N	\N
9d6acdce-211c-4144-b30f-7bb7ea7ba24a	Name 5	001	desc	\N	\N	\N	\N	\N
03e6d4cc-b6c4-4855-9077-b2e94be3c256	Name 6	001	desc	\N	\N	\N	\N	\N
a5b5cceb-dc55-455b-a6d7-1c4dc54d60da	Name 7	001	desc	\N	\N	\N	\N	\N
0eb82695-00bf-465d-87ff-f110f83692db	Name 8	001	desc	\N	\N	\N	\N	\N
fa90308c-bb01-4a6c-81e4-a69f106d7d49	Name 9	001	desc	\N	\N	\N	\N	\N
\.


--
-- TOC entry 2670 (class 2606 OID 16446)
-- Name: tovary Test_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tovary
    ADD CONSTRAINT "Test_pkey" PRIMARY KEY (uid);


-- Completed on 2020-01-04 14:36:56

--
-- PostgreSQL database dump complete
--

