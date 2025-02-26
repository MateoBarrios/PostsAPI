import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Post, PostsService } from '../../services/posts.service';

@Component({
  selector: 'app-post-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  providers: [PostsService],
  templateUrl: './post-list.component.html',
  styleUrl: './post-list.component.css'
})
export class PostListComponent implements OnInit {
  posts: Post[] = [];
  totalCount: number = 0;
  pageNumber: number = 1;
  pageSize: number = 10;

  constructor(private postService: PostsService, private router: Router) {}

  ngOnInit(): void {
    this.loadPosts();
  }

  loadPosts(): void {
    this.postService.getPosts(this.pageNumber, this.pageSize).subscribe({
      next: (data) => {
        this.posts = (data as any)?.posts?.$values ?? []; // Extraer el array correctamente
        this.totalCount = data.totalCount;
      },
      error: (err) => console.error('Error al obtener posts:', err)
    });
  }
  

  verDetalle(id: number) {
    this.router.navigate(['/post', id]);
  }

  nextPage(): void {
    if (this.pageNumber * this.pageSize < this.totalCount) {
      this.pageNumber++;
      this.loadPosts();
    }
  }

  prevPage(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadPosts();
    }
  }
}
