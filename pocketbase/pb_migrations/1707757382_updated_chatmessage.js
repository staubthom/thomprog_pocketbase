/// <reference path="../pb_data/types.d.ts" />
migrate((db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("ec8cpvma1lj1dx1")

  collection.viewRule = ""
  collection.createRule = ""

  return dao.saveCollection(collection)
}, (db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("ec8cpvma1lj1dx1")

  collection.viewRule = "@request.auth.id != \"\""
  collection.createRule = "@request.auth.id != \"\""

  return dao.saveCollection(collection)
})
