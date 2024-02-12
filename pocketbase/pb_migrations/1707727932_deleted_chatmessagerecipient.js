/// <reference path="../pb_data/types.d.ts" />
migrate((db) => {
  const dao = new Dao(db);
  const collection = dao.findCollectionByNameOrId("b6r53n6zqpsirlc");

  return dao.deleteCollection(collection);
}, (db) => {
  const collection = new Collection({
    "id": "b6r53n6zqpsirlc",
    "created": "2024-02-06 05:22:12.905Z",
    "updated": "2024-02-06 05:22:12.905Z",
    "name": "chatmessagerecipient",
    "type": "base",
    "system": false,
    "schema": [
      {
        "system": false,
        "id": "tul7he9e",
        "name": "recipient_id",
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
        "id": "00knhcd1",
        "name": "recipient_group_id",
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
        "id": "bkoeafjp",
        "name": "message_id",
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
        "id": "lcsofp4c",
        "name": "is_read",
        "type": "bool",
        "required": false,
        "presentable": false,
        "unique": false,
        "options": {}
      }
    ],
    "indexes": [],
    "listRule": "",
    "viewRule": "",
    "createRule": "",
    "updateRule": "",
    "deleteRule": "",
    "options": {}
  });

  return Dao(db).saveCollection(collection);
})
