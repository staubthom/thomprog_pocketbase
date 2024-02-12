/// <reference path="../pb_data/types.d.ts" />
migrate((db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("ec8cpvma1lj1dx1")

  // update
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "yyraaw2k",
    "name": "chatconversation_id",
    "type": "relation",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {
      "collectionId": "40ezatb7ejw2ico",
      "cascadeDelete": true,
      "minSelect": null,
      "maxSelect": 1,
      "displayFields": null
    }
  }))

  return dao.saveCollection(collection)
}, (db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("ec8cpvma1lj1dx1")

  // update
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "yyraaw2k",
    "name": "chatconversation_id",
    "type": "relation",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {
      "collectionId": "40ezatb7ejw2ico",
      "cascadeDelete": false,
      "minSelect": null,
      "maxSelect": 1,
      "displayFields": null
    }
  }))

  return dao.saveCollection(collection)
})
