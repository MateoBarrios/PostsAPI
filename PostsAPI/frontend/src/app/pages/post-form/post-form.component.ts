import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PostsService, Category } from '../../services/posts.service';

@Component({
  selector: 'app-post-form',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  providers: [PostsService],
  templateUrl: './post-form.component.html',
  styleUrl: './post-form.component.css'
})
export class PostFormComponent implements OnInit {
  title = '';
  content = '';
  categoryId: number | null = null;
  categories: Category[] = [];
  selectedFile: File | null = null;

  constructor(private postService: PostsService, private router: Router) {}

  ngOnInit(): void {
    this.postService.getCategories().subscribe({
      next: (data) => this.categories = data,
      error: (err) => console.error('Error al obtener categorías:', err)
    });
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] || null;
  }

  createPost() {
    const formData = new FormData();
    formData.append('title', this.title);
    formData.append('content', this.content);
    if (this.categoryId !== null) {
      formData.append('categoryId', this.categoryId.toString());
    }
    if (this.selectedFile) {
      formData.append('file', this.selectedFile);
    }

    this.postService.createPost(formData).subscribe({
      next: (newPost) => {
        console.log('Post creado:', newPost);
        this.router.navigate(['/']); 
      },
      error: (err) => console.error('Error al crear el post:', err)
    });
    
  }
}
