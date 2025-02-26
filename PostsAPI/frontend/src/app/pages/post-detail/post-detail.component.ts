import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { PostsService, Post, Comment } from '../../services/posts.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  imports: [FormsModule,RouterLink,CommonModule],
  styleUrls: ['./post-detail.component.css'],
})
export class PostDetailComponent implements OnInit {
  post: Post | null = null;
  comments: Comment[] = [];
  isEditing = false;
  editedTitle = '';
  editedContent = '';
  newCommentText = '';

  constructor(
    private route: ActivatedRoute,
    private postService: PostsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const postId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadPost(postId);
    this.loadComments(postId);
  }

  loadPost(postId: number): void {
    this.postService.getPostById(postId).subscribe({
      next: (data) => {
        this.post = data;
        this.editedTitle = data.title;
        this.editedContent = data.content;
      },
      error: (err) => console.error('Error al cargar el post:', err)
    });
  }
  

  loadComments(postId: number): void {
    this.postService.getCommentsByPostId(postId).subscribe({
      next: (data) => this.comments = data,
      error: (err) => console.error('Error al cargar comentarios:', err)
    });
  }

  toggleEdit(): void {
    this.isEditing = !this.isEditing;
  }

  updatePost(): void {
    if (!this.post) return;
  
    const updatedPost = {
      title: this.editedTitle,
      content: this.editedContent
    };
  
    this.postService.updatePost(this.post.id, updatedPost).subscribe({
      next: () => {
        this.isEditing = false;
        this.loadPost(this.post!.id);
      },
      error: (err) => console.error('Error al actualizar el post:', err)
    });
  }
  

  deletePost(): void {
    if (!this.post) return;

    this.postService.deletePost(this.post.id).subscribe({
      next: () => this.router.navigate(['/']),
      error: (err) => console.error('Error al eliminar el post:', err)
    });
  }

  addComment(): void {
    if (!this.post || !this.newCommentText.trim()) return;

    this.postService.addComment(this.post.id, this.newCommentText).subscribe({
      next: (newComment) => {
        this.comments.push(newComment);
        this.newCommentText = '';
      },
      error: (err) => console.error('Error al agregar comentario:', err)
    });
  }
}
