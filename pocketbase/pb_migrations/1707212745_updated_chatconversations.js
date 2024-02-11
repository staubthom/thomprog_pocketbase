/// <reference path="../pb_data/types.d.ts" />
migrate((db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("40ezatb7ejw2ico")

  // remove
  collection.schema.removeField("s5vcvvke")

  // remove
  collection.schema.removeField("gpd37ltd")

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "ijjvh5ed",
    "name": "user1_id",
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
    "id": "ybe5vndt",
    "name": "user2_id",
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
  const collection = dao.findCollectionByNameOrId("40ezatb7ejw2ico")

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "s5vcvvke",
    "name": "user1_id",
    "type": "relation",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {
      "collectionId": "_pb_users_auth_",
      "cascadeDelete": false,
      "minSelect": null,
      "maxSelect": 1,
      "displayFields": null
    }
  }))

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "gpd37ltd",
    "name": "user2_id",
    "type": "relation",
    "required": false,
    "presentable": false,
    "unique": false,
    "options": {
      "collectionId": "_pb_users_auth_",
      "cascadeDelete": false,
      "minSelect": null,
      "maxSelect": 1,
      "displayFields": null
    }
  }))

  // remove
  collection.schema.removeField("ijjvh5ed")

  // remove
  collection.schema.removeField("ybe5vndt")

  return dao.saveCollection(collection)
})
