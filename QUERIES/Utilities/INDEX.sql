
-------------------------------------------------------------------------------------
-- DROP INDEX
-------------------------------------------------------------------------------------
DROP INDEX MST_CUSTOMER.IdxCUSTOMER_ID;
DROP INDEX MST_CUSTOMER_DETAIL.IdxCUSTOMER_DETAIL_ID;
DROP INDEX MST_INTRODUCER.IdxINTRODUCER_ID;
DROP INDEX TRN_LEDGER_HEADER.IdxLEDGER_HEADER_ID;
DROP INDEX TRN_LEDGER_DETAIL.IdxLEDGER_DETAIL_ID;
-------------------------------------------------------------------------------------
-- CREATE INDEX
-------------------------------------------------------------------------------------
CREATE UNIQUE INDEX IdxCUSTOMER_ID        ON MST_CUSTOMER        (CUSTOMER_ID);
CREATE UNIQUE INDEX IdxCUSTOMER_DETAIL_ID ON MST_CUSTOMER_DETAIL (CUSTOMER_DETAIL_ID);
CREATE UNIQUE INDEX IdxINTRODUCER_ID      ON MST_INTRODUCER      (INTRODUCER_ID);
CREATE UNIQUE INDEX IdxLEDGER_HEADER_ID   ON TRN_LEDGER_HEADER   (LEDGER_HEADER_ID);
CREATE UNIQUE INDEX IdxLEDGER_DETAIL_ID   ON TRN_LEDGER_DETAIL   (LEDGER_DETAIL_ID);
-------------------------------------------------------------------------------------


