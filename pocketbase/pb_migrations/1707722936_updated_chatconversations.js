/// <reference path="../pb_data/types.d.ts" />
migrate((db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("40ezatb7ejw2ico")

  // remove
  collection.schema.removeField("c1hywaqm")

  // remove
  collection.schema.removeField("ylnnpjcs")

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "en0scfzd",
    "name": "is_read",
    "type": "bool",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {}
  }))

  return dao.saveCollection(collection)
}, (db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("40ezatb7ejw2ico")

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "c1hywaqm",
    "name": "message_id",
    "type": "relation",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {
      "collectionId": "ec8cpvma1lj1dx1",
      "cascadeDelete": false,
      "minSelect": null,
      "maxSelect": 1,
      "displayFields": null
    }
  }))

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "ylnnpjcs",
    "name": "is_read",
    "type": "bool",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {}
  }))

  // remove
  collection.schema.removeField("en0scfzd")

  return dao.saveCollection(collection)
})
