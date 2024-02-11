/// <reference path="../pb_data/types.d.ts" />
migrate((db) => {
  const collection = new Collection({
    "id": "ec8cpvma1lj1dx1",
    "created": "2024-02-05 09:09:41.633Z",
    "updated": "2024-02-05 09:09:41.633Z",
    "name": "chatmessage",
    "type": "base",
    "system": false,
    "schema": [
      {
        "system": false,
        "id": "n2iyayvd",
        "name": "FromID",
        "type": "text",
        "required": false,
        "presentable": false,
        "unique": false,
        "options": {
          "min": null,
          "max": null,
          "pattern": ""
        }
      },
      {
        "system": false,
        "id": "jy6ryryk",
        "name": "message",
        "type": "editor",
        "required": false,
        "presentable": false,
        "unique": false,
        "options": {
          "convertUrls": false
        }
      },
      {
        "system": false,
        "id": "yjblkjlp",
        "name": "conversationID",
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
      }
    ],
    "indexes": [],
    "listRule": null,
    "viewRule": null,
    "createRule": null,
    "updateRule": null,
    "deleteRule": null,
    "options": {}
  });

  return Dao(db).saveCollection(collection);
}, (db) => {
  const dao = new Dao(db);
  const collection = dao.findCollectionByNameOrId("ec8cpvma1lj1dx1");

  return dao.deleteCollection(collection);
})
