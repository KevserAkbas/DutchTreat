import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { LoginRequest, LoginResults } from "../shared/loginResults";
import { Order, OrderItem } from "../shared/Order";
import { Product } from "../shared/Product";

@Injectable()

export class Store {

    constructor(private http: HttpClient) {

    }

    public products: Product[] = [];
    public order: Order = new Order();
    public token = "";
    public expiration = new Date();

    loadProducts(): Observable<void> {
        return this.http.get<[]>("/api/products")
            .pipe(map(data => {
                this.products = data;
                return;
            }));
    }

    get loginRequired(): boolean { //giri� yapman�n gerekli olup olmad���n� kontrol edecek
        return this.token.length === 0 || this.expiration > new Date()
    }

    login(creds: LoginRequest) {
        return this.http.post<LoginResults>("/account/createtoken", creds)
            .pipe(map(data => {
                this.token = data.token;
                this.expiration = data.expiration;
            }));
    }

    addToOrder(product: Product) {

        let item: OrderItem;

        item = this.order.items.find(o => o.productId === product.id);

        if (item) { //eklenen �r�nden daha �nce sepette varsa onun say�s�n� 1 artt�r
            item.quantity++;
        }

        else { // yoksa yeni �r�n� sepete ekle
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