INSERT INTO person(first_name, last_name, tenant_uuid)
    VALUES (@firstName, @lastName, @tenantId)
    RETURNING *;