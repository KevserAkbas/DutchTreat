import { Injectable } from "@angular/core";

@Injectable()

export class Store {
    public products = [{
        title: "Van Gogh Mug",
        price: "19.99"
    }, {
        title: "Van Gogh Poster",
        price: "29.99"
    }];
}