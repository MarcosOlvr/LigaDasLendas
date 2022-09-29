<script setup>
import AllChamps from './components/AllChamps.vue';
import SearchSummoner from './components/SearchSummoner.vue';
</script>

<template>
  <header>
      <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
          <a class="navbar-brand" href="#">Liga das Lendas</a>
          <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
            <div class="navbar-nav">
              <a class="nav-item nav-link active" href="#">Inicio <span class="sr-only">(current)</span></a>
              <a class="nav-item nav-link" href="#">Campeões</a>
              <a class="nav-item nav-link" href="#">Sla krl</a>
            </div>
          </div>
        </div>
      </nav>
  </header>

  <main class="container-fluid">
    <SearchSummoner></SearchSummoner>
    <AllChamps></AllChamps>
  </main>
</template>