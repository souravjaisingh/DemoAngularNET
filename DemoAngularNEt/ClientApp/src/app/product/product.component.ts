import { Component } from '@angular/core';
import { Product } from '../Interfaces/Product';
import { ProductServiceService } from '../product-service.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  products: Product[] = [];
  newProduct: Product = { id: 0, name: '', price: 0 };

  constructor(private productService: ProductServiceService) { }

  ngOnInit(): void {
    this.getAllProducts();
  }

  getAllProducts(): void {
    this.productService.getAllProducts().subscribe(
      (data: Product[]) => (this.products = data),
      (error: any) => console.error('Error fetching products:', error)
    );
  }

  createProduct(): void {
    this.productService.createProduct(this.newProduct).subscribe(
      (data: Product) => {
        this.products.push(data);
        this.newProduct = { id: 0, name: '', price: 0 };
      },
      (error: any) => console.error('Error creating product:', error)
    );
  }

  updateProduct(product: Product): void {
    this.productService.updateProduct(product).subscribe(
      () => console.log('Product updated successfully'),
      (error: any) => console.error('Error updating product:', error)
    );
  }

  deleteProduct(id: number): void {
    this.productService.deleteProduct(id).subscribe(
      () => (this.products = this.products.filter((p) => p.id !== id)),
      (error: any) => console.error('Error deleting product:', error)
    );
  }
}
