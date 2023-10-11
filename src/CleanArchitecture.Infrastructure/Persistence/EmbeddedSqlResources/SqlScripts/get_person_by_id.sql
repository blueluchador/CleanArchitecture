SELECT id, uuid, first_name AS firstName, last_name AS lastName FROM person
WHERE uuid = @uuid;