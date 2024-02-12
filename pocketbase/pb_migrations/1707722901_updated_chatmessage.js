/// <reference path="../pb_data/types.d.ts" />
migrate((db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("ec8cpvma1lj1dx1")

  // remove
  collection.schema.removeField("kmfgmtnq")

  // remove
  collection.schema.removeField("zhb1lhfn")

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "hi6illmf",
    "name": "sender_id",
    "type": "relation",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {
      "collectionId": "6bw0z9s1hdppfyr",
      "cascadeDelete": false,
      "minSelect": null,
      "maxSelect": 1,
      "displayFields": null
    }
  }))

  return dao.saveCollection(collection)
}, (db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("ec8cpvma1lj1dx1")

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "kmfgmtnq",
    "name": "sender_id",
    "type": "relation",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {
      "collectionId": "6bw0z9s1hdppfyr",
      "cascadeDelete": false,
      "minSelect": null,
      "maxSelect": 1,
      "displayFields": null
    }
  }))

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "zhb1lhfn",
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

  // remove
  collection.schema.removeField("hi6illmf")

  return dao.saveCollection(collection)
})
