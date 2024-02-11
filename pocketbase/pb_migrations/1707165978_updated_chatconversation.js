/// <reference path="../pb_data/types.d.ts" />
migrate((db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("9rplm63hc1hlwq6")

  // remove
  collection.schema.removeField("tkglpeeb")

  // remove
  collection.schema.removeField("8pyfiwdv")

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "hqm9ghj5",
    "name": "avatar",
    "type": "file",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {
      "maxSelect": 1,
      "maxSize": 5242880,
      "mimeTypes": [],
      "thumbs": [],
      "protected": false
    }
  }))

  return dao.saveCollection(collection)
}, (db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("9rplm63hc1hlwq6")

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "tkglpeeb",
    "name": "isDone",
    "type": "bool",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {}
  }))

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "8pyfiwdv",
    "name": "todoId",
    "type": "relation",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {
      "collectionId": "9iz1gtvn3vzjeck",
      "cascadeDelete": true,
      "minSelect": null,
      "maxSelect": 1,
      "displayFields": null
    }
  }))

  // remove
  collection.schema.removeField("hqm9ghj5")

  return dao.saveCollection(collection)
})
