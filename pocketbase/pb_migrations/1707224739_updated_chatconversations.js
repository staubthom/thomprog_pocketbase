/// <reference path="../pb_data/types.d.ts" />
migrate((db) => {
  const dao = new Dao(db)
  const collection = dao.findCollectionByNameOrId("40ezatb7ejw2ico")

  // add
  collection.schema.addField(new SchemaField({
    "system": false,
    "id": "hl22fdui",
    "name": "profiles",
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

  // remove
  collection.schema.removeField("hl22fdui")

  return dao.saveCollection(collection)
})
