export class Service{
    array;

    constructor(){
        this.array = [];
    }

    add(object){
        if (this.searchForId(object)){
            object.id = this.array.length;

            this.array.push(object);
        }
        else
            if (typeof(object) === object)
            {
                object.id = this.array.length;
                
                this.array.push(object);
            }
            else{
                console.log("Ввод неверен!");
            }
    }

    searchForId(object){
        for (var key in object){
            if (key == "id"){
                return object;
            }
        }

        return undefined;
    }

    getById(id){
            return this.searchById(id);
    }

    getAll(){
        return this.array;
    }

    deleteById(id){
        let objectToDelete = this.searchById(id);
            if (objectToDelete != null){
                    this.array.splice(id, 1);

                    this.updateArray();

                    return objectToDelete;
            }
        
        return null;
    }

    updateArray(){

        let counter = 1;
        for (let element of this.array){
            element.id = counter;

            counter++;
        }
    }

    updateById(id, newValue){
        newValue.id = id;

        let objectToUpdate = this.searchById(id);
            if (objectToUpdate != null){
                for (let prop in newValue){
                    if (objectToUpdate.hasOwnProperty(prop))
                        objectToUpdate[prop] = newValue[prop];
                }

                return undefined;
            } 
        
        return null;
    }

    replaceById(id, newObject){
        let objectToReplace = this.searchById(id);

        if (objectToReplace != null){
            newObject.id = id;

            this.array.splice(this.array.indexOf(objectToReplace),
            1, newObject);

            return undefined;
        }

        return null;
    }

    searchById(id){
        if (typeof(id) == "string"){
            for (var object of this.array){
                if (object.id == id){
                    return object;
                }
            }
        }
    return null;
    }
}

let arr = new Service();

arr.add({id: 1, name:"Oleg"});

arr.add({id: 1, name:"Ivan"});

arr.add({id: 1, name:"Olesya"});

for (var elem of arr.getAll()){
    console.log(elem.name);
}

console.log(arr.getById(18));

console.log(arr.getById("2"));

console.log("Удалили "+ arr.deleteById("2").name);

console.log(arr.getById("1").name);

arr.updateById("1", {name:"Boris"});

console.log(arr.getById("1").name);

console.log(arr.replaceById("1", {name:"8acuJLuu"}));



