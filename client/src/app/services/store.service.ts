import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Order, OrderItem } from "../shared/Order";
import { Product } from "../shared/Product";

@Injectable()

export class Store {

    constructor(private http: HttpClient) {

    }

    public products: Product[] = [];

    public order: Order = new Order();

    loadProducts(): Observable<void> {
        return this.http.get<[]>("/api/products")
            .pipe(map(data => {
                this.products = data;
                return;
            }));
    }

    addToOrder(product: Product) {

        let item: OrderItem;

        item = this.order.items.find(o => o.productArtId === product.artId);

        if (item) { //eklenen üründen daha önce sepette varsa onun sayýsýný 1 arttýr
            item.quantity++;
        }

        else { // yoksa yeni ürünü sepete ekle
            const item = new OrderItem();

            item.productId = product.id;
            item.productTitle = product.title;
            item.productArtId = product.artId;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productSize = product.category;
            item.unitPrice = product.price;
            item.quantity = 1;

            this.order.items.push(item);
        }


        
    }
}