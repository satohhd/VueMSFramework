<template>
    <div class="d-inline"  :id="_id">
        <template v-if="viewModel[field.name].pageSize > 1">
            <b-button type="button"
                      :class="field.name"
                      v-for="n in viewModel[field.name].pageSize"
                      :key="n" @click.stop.prevent="viewModel[field.name].currentPageNumber = n; listener(section,field)"
                      :variant="viewModel[field.name].currentPageNumber === n ?'success':'outline-success'">
                <span v-html="n"></span>
            </b-button>
        </template>
    </div>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
       ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {

        },
 
    }

</script>
<style scoped>
</style>
